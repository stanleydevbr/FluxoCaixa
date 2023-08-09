using FluxoCaixa.Domain.Enums;
using FluxoCaixa.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluxoCaixa.Api.Adapters
{
    public static class ConsolidadoAdapter
    {
        public static ConsolidadoViewModel ToConsolidadoAdapter(this IEnumerable<CaixaViewModel> viewModel, DateTime data)
        {
            var response = new ConsolidadoViewModel
            {
                Data = data,
                Credito = viewModel.ToList().Where(x => x.Tipo == EnLancamento.Credito).Sum(x => x.Valor),
                Debito = viewModel.ToList().Where(x => x.Tipo == EnLancamento.Debito).Sum(x => x.Valor),
            };

            response.Saldo = response.Credito - response.Debito;
            return response;
        }
    }
}
