using Nuxiba.TestArch.Entities;
using Nuxiba.TestArch.Infraestructure.UnitOfWork;
using System.Data.Entity;

namespace Nuxiba.TestArch.Infraestructure.DbContext
{
    public interface INuxibaDemoDbContext : IEntityFrameworkUnitOfWork
    {
        IDbSet<Usuario> Usuario { get; }
        
        IDbSet<Sexo> Sexo { get; }

        IDbSet<Tarea> Tarea { get; }
    }
}