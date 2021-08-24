using Nuxiba.TestArch.Infraestructure.DbContext;
using System;

namespace Nuxiba.TestArch.Infraestructure.Factories
{
    public interface IDbFactory : IDisposable
    {
        INuxibaDemoDbContext Init();
    }
}