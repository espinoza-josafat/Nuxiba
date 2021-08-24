using Nuxiba.TestArch.Domain.Repositories.Common;
using Nuxiba.TestArch.Domain.UnitOfWork;
using System;
using System.Collections.Generic;

namespace Nuxiba.TestArch.Domain.Services.Common
{
    public class EntityService<TEntity> : IEntityService<TEntity> where TEntity : class
    {
        IUnitOfWork _unitOfWork;
        IRepository<TEntity> _repository;

        public EntityService(IUnitOfWork unitOfWork, IRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork");
            _repository = repository ?? throw new ArgumentNullException("repository");
        }
        
        public virtual void Create(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            _repository.Insert(entity);
            _unitOfWork.Commit();
        }


        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            _repository.Update(entity);
            _unitOfWork.Commit();
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            _repository.Delete(entity);
            _unitOfWork.Commit();
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return _repository.Get();
        }
    }
}