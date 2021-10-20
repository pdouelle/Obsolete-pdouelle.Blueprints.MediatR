using System;
using System.Linq;
using Autofac;
using AutoFixture;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using pdouelle.Blueprint.MediatR.Debug.Domain.ChildEntities.Entities;
using pdouelle.Blueprint.MediatR.Debug.Domain.WeatherForecasts.Entities;
using pdouelle.Blueprints.MediatR;

namespace pdouelle.Blueprint.MediatR.Debug
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddMediatR(typeof(Startup).Assembly);
            services.AddBlueprintMediatR(typeof(Startup).Assembly);
            services.AddAutoMapper(typeof(Startup).Assembly);
            
            services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase("Database"));
            
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "pdouelle.Blueprint.MediatR.Debug", Version = "v1" }); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "pdouelle.Blueprint.MediatR.Debug v1"));
            }

            app.UseHttpsRedirection();
            
            SeedDatabase(context);

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.ConfigureContainer(typeof(DatabaseContext));
        }
        
        private void SeedDatabase(DatabaseContext context)
        {
            context.Database.EnsureCreated();
            
            if (!context.WeatherForecasts.Any())
            {
                var fixture = new Fixture();

                var weatherForecast = new WeatherForecast
                {
                    Date = fixture.Create<DateTime>(),
                    Summary = fixture.Create<string>(),
                    TemperatureC = fixture.Create<int>(),
                    TemperatureF = fixture.Create<int>(),
                };
                
                context.WeatherForecasts.Add(weatherForecast);
                
                context.SaveChanges();

                var childEntity = new ChildEntity
                {
                    WeatherForecastId = weatherForecast.Id,
                    Name = fixture.Create<string>()
                };
                
                context.ChildEntities.Add(childEntity);
                
                context.SaveChanges();
            }
        }
    }
}