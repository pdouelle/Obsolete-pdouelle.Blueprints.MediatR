using Microsoft.EntityFrameworkCore;
using pdouelle.Blueprint.MediatR.Debug.Entities;

namespace pdouelle.Blueprint.MediatR.Debug.Data
{
    public class DatabaseService : DbContext
    {
        public DatabaseService(DbContextOptions<DatabaseService> options) : base(options)
        {
        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}