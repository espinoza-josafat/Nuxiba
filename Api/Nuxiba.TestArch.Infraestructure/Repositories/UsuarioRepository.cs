using Nuxiba.TestArch.Domain.Repositories;
using Nuxiba.TestArch.Entities;
using Nuxiba.TestArch.Infraestructure.Factories;
using Nuxiba.TestArch.Infraestructure.Repositories.Common;
using System.Linq;

namespace Nuxiba.TestArch.Infraestructure.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public Usuario GetByCorreoElectronico(string correoElectronico)
        {
            return _dbSet.FirstOrDefault(x => x.CorreoElectronico == correoElectronico);
        }

        public Usuario GetByUsernameId(string username, int? id = null)
        {
            return id.HasValue ? _dbSet.FirstOrDefault(x => x.Username == username && id != id.Value) : _dbSet.FirstOrDefault(x => x.Username == username);
        }
    }
}