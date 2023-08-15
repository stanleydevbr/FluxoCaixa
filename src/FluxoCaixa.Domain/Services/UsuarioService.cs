using AutoMapper;
using FluxoCaixa.Domain.Entities;
using FluxoCaixa.Domain.Interfaces.Handlers;
using FluxoCaixa.Domain.Interfaces.Repositories;
using FluxoCaixa.Domain.Interfaces.Services;
using FluxoCaixa.Domain.Notifications;
using FluxoCaixa.Domain.ViewModels;
using System;
using System.Linq;

namespace FluxoCaixa.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly NotificationContext _notificationContext;
        private readonly IMapper _mapper;
        private readonly IJwtHandler _jwtHandler;

        public UsuarioService(IUsuarioRepository repository, IMapper mapper, NotificationContext notificationContext, IJwtHandler jwtHandler)
        {
            _repository = repository;
            _mapper = mapper;
            _notificationContext = notificationContext;
            _jwtHandler = jwtHandler;
        }

        public void Adicionar(UsuarioViewModel model)
        {
            var usuario = _mapper.Map<Usuario>(model);
            _repository.Adicionar(usuario);
        }

        public void Atualizar(UsuarioViewModel model)
        {
            var usuario = _mapper.Map<Usuario>(model);
            _repository.Atualizar(usuario);
        }

        public UsuarioLogadoViewModel Login(LoginViewModel login)
        {
            var usuarioLogado = default(UsuarioLogadoViewModel);
            var usuario = _repository.Buscar(x => x.Email == login.Email).FirstOrDefault();

            if (usuario?.Email != login.Email || usuario?.Senha != login.Senha)
            {
                _notificationContext.AddNotification(new Notification("usuario", "E-mail ou senha inválido."));
                return usuarioLogado;
            }

            return _jwtHandler.GerarToken(usuario);
        }

        public UsuarioViewModel ObterPorEmail(string email)
        {
            var usuario = _repository.Buscar(x => x.Email == email)?.FirstOrDefault();
            var model = _mapper.Map<UsuarioViewModel>(usuario);
            return model;
        }

        public void Remover(long id)
        {
            _repository.Remover(id);
        }


    }
}
