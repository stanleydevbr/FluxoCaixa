using FluxoCaixa.Domain.Entities;
using FluxoCaixa.Domain.ViewModels;

namespace FluxoCaixa.Domain.Interfaces.Handlers
{
    public interface IJwtHandler
    {
        UsuarioLogadoViewModel GerarToken(Usuario usuario);
    }
}
