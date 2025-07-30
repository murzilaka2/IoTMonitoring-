using Confluent.Kafka;
using Microsoft.AspNetCore.SignalR;
using Monitoring.Api.Hubs;
using Monitoring.Api.Models;
using System.Text.Json;

namespace Monitoring.Api.Services
{
    public class SensorConsumer : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHubContext<SensorHub> _hub;
        private readonly IConsumer<Null, string> _consumer;
        private readonly string _topic;
        private readonly ILogger<SensorConsumer> _logger;

        public SensorConsumer(IConfiguration config, IServiceScopeFactory scopeFactory, IHubContext<SensorHub> hub, ILogger<SensorConsumer> logger)
        {
            _scopeFactory = scopeFactory;
            _hub = hub;
            _logger = logger;

            var cfg = new ConsumerConfig
            {
                BootstrapServers = config["Kafka:BootstrapServers"],
                GroupId = config["Kafka:GroupId"],
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true
            };

            _topic = config["Kafka:Topic"]!;
            _consumer = new ConsumerBuilder<Null, string>(cfg).Build();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(async () =>
            {
                try
                {
                    _logger.LogInformation("SensorConsumer запущен");

                    _consumer.Subscribe(_topic);

                    while (!stoppingToken.IsCancellationRequested)
                    {
                        try
                        {
                            var result = _consumer.Consume(TimeSpan.FromMilliseconds(500));
                            if (result == null || string.IsNullOrWhiteSpace(result.Message?.Value))
                                continue;

                            var data = JsonSerializer.Deserialize<SensorData>(
                                result.Message.Value,
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                            if (data != null)
                            {
                                await _hub.Clients.All.SendAsync("NewReading", data, stoppingToken);
                                _logger.LogInformation($"[{data.SensorId}] {data.Temperature} °C @ {data.Timestamp}");
                            }
                        }
                        catch (ConsumeException ce)
                        {
                            _logger.LogError(ce, "Kafka consume error");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Ошибка в SensorConsumer цикле");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Критическая ошибка SensorConsumer");
                }
                finally
                {
                    _consumer.Close();
                    _logger.LogInformation("SensorConsumer остановлен.");
                }
            }, stoppingToken);

            return Task.CompletedTask; // Уведомляем хост: сервис "запущен"
        }
    }
}
