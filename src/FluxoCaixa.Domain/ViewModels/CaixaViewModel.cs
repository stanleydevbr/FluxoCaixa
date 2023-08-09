using FluxoCaixa.Domain.Enums;
using System;

namespace FluxoCaixa.Domain.ViewModels
{
    public class CaixaViewModel: EntityViewModel
    {
        public DateTime Lancamento { get; set; } = DateTime.Now;
        public string Descricao { get; set; }
        public EnLancamento Tipo { get; set; }
        public decimal Valor { get; set; }
    }

    public class ConsolidadoViewModel
    {
        public DateTime Data { get; set; }
        public decimal Credito { get; set; }
        public decimal Debito { get; set; }
        public decimal Saldo { get; set; }
    }
}
