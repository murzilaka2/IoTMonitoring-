using Microsoft.EntityFrameworkCore;
using Monitoring.Api.Models;

namespace Monitoring.Api.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<SensorData> SensorReadings { get; set; }
    }
}
