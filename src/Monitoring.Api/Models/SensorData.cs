namespace Monitoring.Api.Models
{
    public class SensorData
    {
        public int Id { get; set; }
        public string SensorId { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
    }
}
