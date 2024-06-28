using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OfficeManager.Infra.Context;

namespace OfficeManager.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            var connectionString = "Server=localhost\\SQLEXPRESS;Database=OfficeManager;User Id=sa;Password=sa;MultipleActiveResultSets=true;Application Name=OfficeManager";

            services.AddDbContext<OfficeManagerContext>(options => options.UseSqlServer(connectionString));

            ConfigureSwagger(services);

            services.RegisterServices();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (app != null)
            {
                var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

                var context = serviceScope.ServiceProvider.GetRequiredService<OfficeManagerContext>();
                context.Database.Migrate();

                app.UsePathBase("/api/office-manager");
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API do MeuProjeto v1");
                    c.RoutePrefix = string.Empty; // Para acessar o Swagger na raiz (localhost:<porta>/)
                });
            }
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Office Manager's API",
                });
            });
        }
    }
}