using Nuxiba.TestArch.Domain.Repositories;
using Nuxiba.TestArch.Domain.Services;
using Nuxiba.TestArch.Domain.Services.Common;
using Nuxiba.TestArch.Domain.UnitOfWork;
using Nuxiba.TestArch.Entities;

namespace Nuxiba.TestArch.Application.Services
{
    public class SexoService : EntityService<Sexo>, ISexoService
    {
        IUnitOfWork _unitOfWork;
        ISexoRepository _sexoRepository;

        public SexoService(IUnitOfWork unitOfWork, ISexoRepository sexoRepository)
            : base(unitOfWork, sexoRepository)
        {
            _unitOfWork = unitOfWork;
            _sexoRepository = sexoRepository;
        }
    }
}