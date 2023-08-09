using AutoMapper;
using FluxoCaixa.Domain.Entities;
using FluxoCaixa.Domain.Interfaces.Repositories;
using FluxoCaixa.Domain.Interfaces.Services;
using FluxoCaixa.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluxoCaixa.Domain.Services
{
    public class CaixaService : ICaixaService
    {
        private readonly ICaixaRepository _repository;
        private readonly IMapper _mapper;

        public CaixaService(ICaixaRepository respository, IMapper mapper)
        {
            _repository = respository;
            _mapper = mapper;
        }

        public void Gravar(CaixaViewModel model)
        {
            var caixa = _mapper.Map<Caixa>(model);
            //TODO: Incluir verificação de validação
            _repository.Adicionar(caixa);
        }

        public CaixaViewModel ObterLancamentoPorId(long id)
        {
            var resultado = _repository.ObterPorId(id);
            var response = _mapper.Map<CaixaViewModel>(resultado);
            return response;
        }

        public IEnumerable<CaixaViewModel> ObterLancamentosPorPeriodo(DateTime Inicio, DateTime Termino)
        {
            var resultado = _repository.Buscar(x => x.Lancamento.Date >= Inicio && x.Lancamento.Date <= Termino);
            var response = _mapper.Map<List<CaixaViewModel>>(resultado);
            return response;

        }

        public IEnumerable<CaixaViewModel> ObterTodosLancamentos()
        {
            var resultado = _repository.ObterTodos().ToList();
            var response = _mapper.Map<List<CaixaViewModel>>(resultado);
            return response;
        }

        public void RemoverLancamento(long id)
        {
            _repository.Remover(id);
        }
    }
}
