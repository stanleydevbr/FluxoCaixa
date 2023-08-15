using FluxoCaixa.Domain.ViewModels;

namespace FluxoCaixa.Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        public void Adicionar(UsuarioViewModel usuario);
        public void Atualizar(UsuarioViewModel caixa);
        public UsuarioViewModel ObterPorEmail(string email);
        public void Remover(long id);
        public UsuarioLogadoViewModel Login(LoginViewModel login);
    }
}
