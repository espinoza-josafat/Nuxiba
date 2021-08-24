using Nuxiba.TestArch.Domain.Repositories.Common;
using Nuxiba.TestArch.Entities;

namespace Nuxiba.TestArch.Domain.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario GetByCorreoElectronico(string correoElectronico);

        Usuario GetByUsernameId(string username, int? id = null);
    }
}