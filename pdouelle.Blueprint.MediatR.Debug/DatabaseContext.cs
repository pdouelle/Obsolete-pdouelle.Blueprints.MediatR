using Microsoft.EntityFrameworkCore;
using pdouelle.Blueprint.MediatR.Debug.Domain.ChildEntities.Entities;
using pdouelle.Blueprint.MediatR.Debug.Domain.WeatherForecasts.Entities;

namespace pdouelle.Blueprint.MediatR.Debug
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DbSet<ChildEntity> ChildEntities { get; set; }
    }
}