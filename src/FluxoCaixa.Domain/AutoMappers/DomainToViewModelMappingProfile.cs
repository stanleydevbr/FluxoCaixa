using AutoMapper;
using FluxoCaixa.Domain.Entities;
using FluxoCaixa.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluxoCaixa.Domain.AutoMappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Caixa, CaixaViewModel>();
        }
    }
}
