using Nuxiba.TestArch.Infraestructure.DbContext;

namespace Nuxiba.TestArch.Infraestructure.Factories
{
    public class DbFactory : Disposable, IDbFactory
    {
        INuxibaDemoDbContext _unitOfWork;

        public INuxibaDemoDbContext Init()
        {
            if (_unitOfWork == null)
                _unitOfWork = new NuxibaDemoDbContext();

            return _unitOfWork;
        }

        protected override void DisposeCore()
        {
            if (_unitOfWork != null)
                _unitOfWork.Dispose();
        }
    }
}