using FluxoCaixa.Domain.Enums;
using FluxoCaixa.Domain.ViewModels;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace FluxoCaixa.Api.Docs
{
    public class CaixaExample : IExamplesProvider<CaixaViewModel>
    {
        public CaixaViewModel GetExamples()
        {
            return new CaixaViewModel
            {
                Id = 9,
                Descricao = "Produto X1",
                Lancamento = DateTime.Now,
                Tipo = EnLancamento.Credito,
                Valor = 190.04m
            };
        }
    }
}
