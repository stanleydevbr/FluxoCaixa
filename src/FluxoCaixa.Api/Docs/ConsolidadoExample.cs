using FluxoCaixa.Domain.ViewModels;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace FluxoCaixa.Api.Docs
{
    public class ConsolidadoExample : IExamplesProvider<ConsolidadoViewModel>
    {
        public ConsolidadoViewModel GetExamples()
        {
            var consolidado = new ConsolidadoViewModel
            {
                Data = DateTime.Now.Date,
                Credito = 1010.05m,
                Debito = 3550.02m,
            };
            consolidado.Saldo = consolidado.Credito - consolidado.Debito;
            return consolidado;
        }
    }
}
