using FluxoCaixa.Domain.Entities;
using FluxoCaixa.Domain.ViewModels;
using System;
using System.Collections.Generic;

namespace FluxoCaixa.Domain.Interfaces.Services
{
    public interface ICaixaService
    {
        public void Gravar(CaixaViewModel caixa);
        public void Atualizar(CaixaViewModel caixa);
        public CaixaViewModel ObterLancamentoPorId(long id);
        public IEnumerable<CaixaViewModel> ObterLancamentosPorPeriodo(DateTime Inicio, DateTime Termino);
        public IEnumerable<CaixaViewModel> ObterTodosLancamentos();
        public void Remover(long id);
    }
}
