using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TccSite.Data.Context;
using Microsoft.EntityFrameworkCore;
using TccSite.Domain.Interfaces;
using TccSite.Infrastructure.Repository;

namespace TccSite.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAlertaRepository, AlertaRepository>();
            services.AddScoped<ICidadeRepository, CidadeRepository>();
            services.AddScoped<IConfiguracoesRepository, ConfiguracoesRepository>();
            services.AddScoped<IEstadoRepository, EstadoRepository>();
            services.AddScoped<IImagensEsp32Repository, ImagensEsp32Repository>();
            services.AddScoped<ILogDeAcessosRepository, LogDeAcessosRepository>();
            services.AddScoped<IPessoaCadastroRepository, PessoaCadastroRepository>();
            services.AddScoped<IStatusAlertaRepository, StatusAlertaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            return services;
        }
    }
}
