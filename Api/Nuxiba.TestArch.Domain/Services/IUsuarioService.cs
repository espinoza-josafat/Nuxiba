using Nuxiba.TestArch.Domain.Services.Common;
using Nuxiba.TestArch.Entities;

namespace Nuxiba.TestArch.Domain.Services
{
    public interface IUsuarioService : IEntityService<Usuario>
    {
        Usuario GetById(int id);

        Usuario GetByCorreoElectronico(string correoElectronico);

        Usuario GetByUsernameId(string username, int? id = null);
    }
}