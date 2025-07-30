using Confluent.Kafka;
using System.Text.Json;

var config = new ProducerConfig { BootstrapServers = "localhost:29092" };
using var producer = new ProducerBuilder<Null, string>(config).Build();

var rnd = new Random();
while (true)
{
    var data = new
    {
        SensorId = $"sensor-{rnd.Next(1, 5)}",
        Timestamp = DateTime.UtcNow,
        Temperature = Math.Round(15 + rnd.NextDouble() * 10, 2),
        Humidity = Math.Round(30 + rnd.NextDouble() * 50, 2)
    };
    var json = JsonSerializer.Serialize(data);
    var message = new Message<Null, string> { Value = json };
    var deliveryResult = await producer.ProduceAsync("sensor-data", message);
    Console.WriteLine($"Отправлено в {deliveryResult.TopicPartitionOffset}: {json}");
    await Task.Delay(1000);
}


