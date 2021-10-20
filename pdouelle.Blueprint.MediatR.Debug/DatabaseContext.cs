using Microsoft.EntityFrameworkCore;
using pdouelle.Blueprint.MediatR.Debug.Entities;

namespace pdouelle.Blueprint.MediatR.Debug
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}