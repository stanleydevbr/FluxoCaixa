using AutoMapper;
using FluxoCaixa.Domain.Entities;
using FluxoCaixa.Domain.ViewModels;

namespace FluxoCaixa.Domain.AutoMappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Caixa, CaixaViewModel>();
            CreateMap<Usuario, UsuarioViewModel>();
        }
    }
}
