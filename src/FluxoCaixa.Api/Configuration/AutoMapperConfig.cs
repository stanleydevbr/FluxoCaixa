using AutoMapper;
using FluxoCaixa.Domain.AutoMappers;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoCaixa.Api.Configuration
{
    public static class AutoMapperConfig
    {
        public static void AutoMapperRegister(this IServiceCollection service)
        {
            var config = new MapperConfiguration(cfg =>
           {
               cfg.AddProfile(new DomainToViewModelMappingProfile());
               cfg.AddProfile(new ViewModelToDomainMappingProfile());
           });
            IMapper mapper = config.CreateMapper();
            service.AddSingleton(mapper);
        }
    }
}
