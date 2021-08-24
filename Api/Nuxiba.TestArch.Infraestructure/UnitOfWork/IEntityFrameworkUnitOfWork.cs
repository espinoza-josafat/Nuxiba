using Nuxiba.TestArch.Domain.UnitOfWork;
using System.Collections.Generic;
using System.Data.Entity;

namespace Nuxiba.TestArch.Infraestructure.UnitOfWork
{
    public interface IEntityFrameworkUnitOfWork : IUnitOfWork
    {
        //Commit sobre la base de datos. Si hay un problema de concurrencia "refrescará" los datos del cliente. Aproximación "Client wins"
        void CommitAndRefreshChanges();

        //Rollback de los cambios que se han producido en la Unit of Work y que están siendo observados por ella
        void Rollback();



        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        void Attach<TEntity>(TEntity entity) where TEntity : class;

        void SetModified<TEntity>(TEntity entity) where TEntity : class;

        IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters);

        int ExecuteCommand(string sqlCommand, params object[] parameters);
    }
}