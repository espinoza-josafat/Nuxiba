using Nuxiba.TestArch.Domain.Services.Common;
using Nuxiba.TestArch.Entities;

namespace Nuxiba.TestArch.Domain.Services
{
    public interface ITareaService : IEntityService<Tarea>
    {
        Tarea GetById(int id);
    }
}
