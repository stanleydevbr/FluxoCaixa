using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluxoCaixa.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> : IDisposable
    {
        TEntity Adicionar(TEntity obj);
        TEntity Atualizar(TEntity obj);
        TEntity ObterPorId(long id);
        IEnumerable<TEntity> ObterTodos();
        IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate);
        bool Remover(long id);
    }
}
