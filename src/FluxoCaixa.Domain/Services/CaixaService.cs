using AutoMapper;
using FluxoCaixa.Domain.Entities;
using FluxoCaixa.Domain.Interfaces.Repositories;
using FluxoCaixa.Domain.Interfaces.Services;
using FluxoCaixa.Domain.Notifications;
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
        private readonly NotificationContext _notificationContext;

        public CaixaService(ICaixaRepository respository, IMapper mapper, NotificationContext notificationContext)
        {
            _repository = respository;
            _mapper = mapper;
            _notificationContext = notificationContext;
        }

        public void Atualizar(CaixaViewModel model)
        {
            var caixa = _mapper.Map<Caixa>(model);
            if (!caixa.EhValido())
                _notificationContext.AddNotifications(caixa.ValidationResult);

            if (_repository.ObterPorId(model.Id) == null)
                _notificationContext.AddNotification(new Notification("Id", $"Não foi encontrado lançamento para o Id informado {model.Id}."));
            else
                _repository.Atualizar(caixa);
        }

        public void Gravar(CaixaViewModel model)
        {
            var caixa = _mapper.Map<Caixa>(model);
            if (!caixa.EhValido())
                _notificationContext.AddNotifications(caixa.ValidationResult);

            if (_repository.ObterPorId(model.Id) == null)
                _repository.Adicionar(caixa);
            else
                _notificationContext.AddNotification(new Notification("Id", $"O lançamento já existe para o Id informado {model.Id}."));
        }

        public CaixaViewModel ObterLancamentoPorId(long id)
        {
            var response = default(CaixaViewModel);

            var resultado = _repository.ObterPorId(id);
            if (resultado == null)
                _notificationContext.AddNotification(new Notification("Id", $"Lançamento não encontrado para este Id {id}."));
            else
                response = _mapper.Map<CaixaViewModel>(resultado);
            return response;
        }

        public IEnumerable<CaixaViewModel> ObterLancamentosPorPeriodo(DateTime Inicio, DateTime Termino)
        {
            var response = default(List<CaixaViewModel>);
            var resultado = _repository.Buscar(x => x.Lancamento.Date >= Inicio && x.Lancamento.Date <= Termino);

            if (resultado.Count() == 0)
                _notificationContext.AddNotification(new Notification("Lancamento", $"Não foi encontrado lançamento para o período informado: {Inicio.ToShortDateString()}."));
            else
                response = _mapper.Map<List<CaixaViewModel>>(resultado);

            return response;
        }

        public IEnumerable<CaixaViewModel> ObterTodosLancamentos()
        {
            var resultado = _repository.ObterTodos().ToList();
            var response = default(List<CaixaViewModel>);
            if (resultado.Count() == 0)
                _notificationContext.AddNotification(new Notification("Lancamento", $"Não foi encontrado lançamento na base de dados."));
            else
                response = _mapper.Map<List<CaixaViewModel>>(resultado);
            return response;
        }

        public void Remover(long id)
        {
            if (_repository.ObterPorId(id) == null)
                _notificationContext.AddNotification(new Notification("Id", $"O lançamento não existe para ser excluído Id {id}."));
            else
                _repository.Remover(id);
        }
    }
}
