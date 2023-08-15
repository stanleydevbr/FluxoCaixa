using AutoMapper;
using FluxoCaixa.Domain.Entities;
using FluxoCaixa.Domain.ViewModels;

namespace FluxoCaixa.Domain.AutoMappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CaixaViewModel, Caixa>();
            CreateMap<UsuarioViewModel, Usuario>();
        }
    }
}
