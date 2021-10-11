using Autofac;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using pdouelle.Blueprint.MediatR.Debug.Data;
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
            services.AddControllers().AddNewtonsoftJson();

            services.AddMediatR(typeof(Startup).Assembly);
            services.AddBlueprintMediatR(typeof(Startup).Assembly);
            services.AddAutoMapper(typeof(Startup).Assembly);
            
            var connectionString = Configuration.GetConnectionString("Database");
            services.AddDbContext<DatabaseService>(options => { options.UseSqlServer(connectionString); });
            
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "pdouelle.Blueprint.MediatR.Debug", Version = "v1" }); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "pdouelle.Blueprint.MediatR.Debug v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.ConfigureContainer(typeof(DatabaseService));
        }
    }
}