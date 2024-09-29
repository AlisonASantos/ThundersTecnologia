using ThundersTecnologia.Business.Intefaces;
using ThundersTecnologia.Business.Services;
using ThundersTecnologia.Data.Context;
using ThundersTecnologia.Data.Repository;

namespace ThundersTecnologia.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddScoped<ITarefaRepository, TarefaRepository>();
            services.AddScoped<ITarefaService, TarefaService>();

            return services;
        }
    }
}