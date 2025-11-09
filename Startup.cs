using System.Text.Json.Serialization;
using GerenciadorDeTarefas.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GerenciadorDeTarefas
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }

        // Registros de serviços (IoC)
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

            services.AddSwaggerGen();

            // Camada de regras de negócio
            services.AddSingleton<ITarefaService, TarefaService>();
        }

        // Pipeline HTTP
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GerenciadorDeTarefas v1");
                    c.RoutePrefix = "swagger"; // abre em /swagger
                });
            }
            else
            {
                // Em produção você pode manter Swagger, se quiser
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GerenciadorDeTarefas v1");
                    c.RoutePrefix = "swagger";
                });
            }

            app.UseRouting();
            // app.UseAuthorization(); // se adicionar Auth depois

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
