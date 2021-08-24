using Nuxiba.TestArch.Domain.Repositories;
using Nuxiba.TestArch.Domain.Services;
using Nuxiba.TestArch.Domain.Services.Common;
using Nuxiba.TestArch.Domain.UnitOfWork;
using Nuxiba.TestArch.Entities;

namespace OneCore.Demo.Application.Services
{
    public class TareaService : EntityService<Tarea>, ITareaService
    {
        IUnitOfWork _unitOfWork;
        ITareaRepository _tareaRepository;

        public TareaService(IUnitOfWork unitOfWork, ITareaRepository tareaRepository)
            : base(unitOfWork, tareaRepository)
        {
            _unitOfWork = unitOfWork;
            _tareaRepository = tareaRepository;
        }

        public Tarea GetById(int id)
        {
            return _tareaRepository.GetById(id);
        }
    }
}
