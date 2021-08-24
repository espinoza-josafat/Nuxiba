using Nuxiba.TestArch.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Nuxiba.TestArch.Infraestructure.DbContext
{
    public class NuxibaDemoDbContext : System.Data.Entity.DbContext, INuxibaDemoDbContext
    {
        public NuxibaDemoDbContext() : base("NuxibaDemoDbContext")
        {
            //Disable initializer
            Database.SetInitializer<NuxibaDemoDbContext>(null);
        }
        
        IDbSet<Usuario> _usuario;

        public IDbSet<Usuario> Usuario
        {
            get
            {
                if (_usuario == null)
                    _usuario = base.Set<Usuario>();

                return _usuario;
            }
        }

        IDbSet<Sexo> _sexo;

        public IDbSet<Sexo> Sexo
        {
            get
            {
                if (_sexo == null)
                    _sexo = base.Set<Sexo>();

                return _sexo;
            }
        }

        IDbSet<Tarea> _tarea;

        public IDbSet<Tarea> Tarea
        {
            get
            {
                if (_tarea == null)
                    _tarea = base.Set<Tarea>();

                return _tarea;
            }
        }

        // Sobreescribimos el método OnModelCreating de la clase DbContext
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configuration.AutoDetectChangesEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : class
        {
            if (Entry(entity).State == EntityState.Detached)
            {
                base.Set<TEntity>().Attach(entity);
            }
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        // Implementación de IUnitOfWork
        public void SetModified<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void Commit()
        {
            base.SaveChanges();
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    base.SaveChanges();

                    saveFailed = false;

                }
                catch (DbUpdateConcurrencyException exception)
                {
                    saveFailed = true;

                    exception.Entries.ToList()
                                     .ForEach(entry =>
                                     {
                                         entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                                     });
                }
            } while (saveFailed);
        }

        public void Rollback()
        {
            ChangeTracker.Entries()
                         .ToList()
                         .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        public new void Dispose()
        {
            base.Dispose();
        }
    }
}