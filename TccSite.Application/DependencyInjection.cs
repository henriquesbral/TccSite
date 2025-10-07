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
            services.AddTransient<IAlertaService, AlertaService>();

            return services;
        }
    }
}
