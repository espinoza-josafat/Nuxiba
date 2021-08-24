using System;

namespace Nuxiba.TestArch.Domain.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        //Commit sobre la base de datos. Si hay un problema de concurrencia lanzará una excepción
        void Commit();
    }
}