using Nuxiba.TestArch.Domain.Repositories;
using Nuxiba.TestArch.Entities;
using Nuxiba.TestArch.Infraestructure.Factories;
using Nuxiba.TestArch.Infraestructure.Repositories.Common;

namespace Nuxiba.TestArch.Infraestructure.Repositories
{
    public class SexoRepository : Repository<Sexo>, ISexoRepository
    {
        public SexoRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}