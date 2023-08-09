using FluxoCaixa.Domain.Interfaces.Repositories;
using FluxoCaixa.Domain.Interfaces.Services;
using FluxoCaixa.Domain.Services;
using FluxoCaixa.Infra.Data.Context;
using FluxoCaixa.Infra.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoCaixa.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICaixaService, CaixaService>();
            services.AddTransient<ICaixaRepository, CaixaRepository>();
            
        }
    }

}
