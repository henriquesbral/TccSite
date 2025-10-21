using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TccSite.Application.Interfaces;
using TccSite.Application.Services;

namespace TccSite.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAlertaService, AlertaService>();
            services.AddScoped<ICidadeService, CidadeService>();
            services.AddScoped<IConfiguracoesService, ConfiguracoesService>();
            services.AddScoped<IEstadoService, EstadoService>();
            services.AddScoped<IImagensEsp32Service, ImagensEsp32Service>();
            services.AddScoped<ILogDeAcessosService, LogDeAcessosService>();
            services.AddScoped<IPessoaCadastroService, PessoaCadastroService>();
            services.AddScoped<IStatusAlertaService, StatusAlertaService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IUsuarioSenhaService, UsuarioSenhaService>();

            return services;
        }
    }
}
