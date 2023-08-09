using AutoMapper;
using FluxoCaixa.Domain.Entities;
using FluxoCaixa.Domain.ViewModels;
using System.Collections.Generic;

namespace FluxoCaixa.Domain.AutoMappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CaixaViewModel, Caixa>();
        }
    }
}
