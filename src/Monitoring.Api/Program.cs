using Microsoft.EntityFrameworkCore;
using Monitoring.Api.Data;
using Monitoring.Api.Hubs;
using Monitoring.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(opts =>
    opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500") // Адрес нашего клиента
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddHostedService<SensorConsumer>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    db.Database.Migrate();
}

app.UseRouting();
app.UseCors();
app.UseHttpsRedirection();
app.MapHub<SensorHub>("/sensorHub");

app.MapGet("/test", async (context) => await context.Response.WriteAsync("Works!"));

app.Run();

