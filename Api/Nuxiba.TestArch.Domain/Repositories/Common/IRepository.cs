using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Nuxiba.TestArch.Domain.Repositories.Common
{
    /// <summary>
    /// Métodos genéricos para todos los repositorios
    /// </summary>
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        TEntity GetById(object id);

        void Insert(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);

        IEnumerable<TEntity> GetPagedElements<TKey>(int pageIndex, int pageCount, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending = true);

        IEnumerable<TEntity> GetPagedElements<TKey>(int pageIndex, int pageCount, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending = true, string includeProperties = "");

        IEnumerable<TEntity> GetFromDatabaseWithQuery(string sqlQuery, params object[] parameters);

        int ExecuteInDatabaseByQuery(string sqlCommand, params object[] parameters);
    }
}