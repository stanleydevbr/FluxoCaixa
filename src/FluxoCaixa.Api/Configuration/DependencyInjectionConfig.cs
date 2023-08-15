using FluxoCaixa.Domain.Handlers;
using FluxoCaixa.Domain.Interfaces.Handlers;
using FluxoCaixa.Domain.Interfaces.Repositories;
using FluxoCaixa.Domain.Interfaces.Services;
using FluxoCaixa.Domain.Notifications;
using FluxoCaixa.Domain.Services;
using FluxoCaixa.Infra.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoCaixa.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<NotificationContext>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICaixaService, CaixaService>();
            services.AddTransient<ICaixaRepository, CaixaRepository>();

            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IJwtHandler, JwtHandler>();

        }
    }

}
