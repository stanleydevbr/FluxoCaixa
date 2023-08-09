using FluxoCaixa.Domain.Entities;
using FluxoCaixa.Domain.Interfaces.Repositories;
using FluxoCaixa.Infra.Data.Context;

namespace FluxoCaixa.Infra.Data.Repositories
{
    public class CaixaRepository : RepositoryBase<Caixa>, ICaixaRepository
    {
        public CaixaRepository(FluxoCaixaContext context) : base(context)
        {
        }
    }
}
