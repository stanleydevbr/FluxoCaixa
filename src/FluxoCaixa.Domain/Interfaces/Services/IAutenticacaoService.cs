using FluxoCaixa.Domain.ViewModels;

namespace FluxoCaixa.Domain.Interfaces.Services
{
    public interface IAutenticacaoService
    {
        public UsuarioLogadoViewModel Login(LoginViewModel login);
    }
}
