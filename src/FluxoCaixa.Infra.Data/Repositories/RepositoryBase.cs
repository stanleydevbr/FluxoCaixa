using FluxoCaixa.Domain.Entities;
using FluxoCaixa.Domain.Interfaces.Repositories;
using FluxoCaixa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FluxoCaixa.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : Entity<TEntity>
    {
        protected FluxoCaixaContext Db;
        protected DbSet<TEntity> DbSet;

        public RepositoryBase(FluxoCaixaContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual TEntity Adicionar(TEntity obj)
        {
            DbSet.Add(obj);
            Db.Commit();
            return obj;
        }

        public virtual TEntity Atualizar(TEntity obj)
        {
            DbSet.Update(obj);
            Db.Commit();
            return obj;
        }

        public virtual TEntity ObterPorId(long id)
        {
            return DbSet.AsNoTracking().FirstOrDefault(t => t.Id == id);
        }

        public virtual IEnumerable<TEntity> ObterTodos()
        {
            return DbSet.ToList();
        }

        public virtual IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate).ToList();
        }

        public virtual bool Remover(long id)
        {
            DbSet.Remove(DbSet.Find(id));
            Db.SaveChanges();
            return true;
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
