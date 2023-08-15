using FluxoCaixa.Domain.Entities;
using FluxoCaixa.Domain.Interfaces.Repositories;
using FluxoCaixa.Infra.Data.Context;

namespace FluxoCaixa.Infra.Data.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(FluxoCaixaContext context) : base(context)
        {
        }
    }
}
