using System.Collections.Generic;

namespace Nuxiba.TestArch.Domain.Services.Common
{
    public interface IEntityService<TEntity> : IService
     where TEntity : class
    {
        void Create(TEntity entity);

        void Delete(TEntity entity);

        IEnumerable<TEntity> Get();

        void Update(TEntity entity);
    }
}