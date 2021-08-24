using Nuxiba.TestArch.Domain.Repositories.Common;
using Nuxiba.TestArch.Infraestructure.DbContext;
using Nuxiba.TestArch.Infraestructure.Factories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Nuxiba.TestArch.Infraestructure.Repositories.Common
{
    /// <summary>
    /// Implementacion de un repositorio generico
    /// </summary>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly INuxibaDemoDbContext _dbContext;
        protected readonly IDbSet<TEntity> _dbSet;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dbFactory">DataBase factory</param>
        protected Repository(IDbFactory dbFactory)
        {
            if (dbFactory == null)
                throw new ArgumentNullException("dbFactory");

            _dbContext = dbFactory.Init();
            _dbSet = _dbContext.Set<TEntity>();
        }

        /// <summary>
        /// Método genérico para recuperar una coleccion de entidades
        /// </summary>
        /// <param name="filter">Expresion para filtrar las entidades</param>
        /// <param name="orderBy">Orden en el que se quiere recuperar las entidades</param>
        /// <param name="includeProperties">Propiedades de Navegacion a incluir</param>
        /// <returns>Un listado de objetos de la entidad genérica</returns>
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        /// <summary>
        /// Metodo generico para recuperar una entidad a partir de su identidad
        /// </summary>
        /// <param name="id">La identidad de la entidad</param>
        /// <returns>La entidad</returns>
        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Metodo generico para añadir una entidad al contexto de trabajo
        /// </summary>
        /// <param name="entity">La entidad para añadir</param>
        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// Metodo generico para eliminar una entidad del contexto de trabajo
        /// </summary>
        /// <param name="id">La identidad de la entidad</param>
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Metodo generico para eliminar una entidad del contexto de trabajo pasandole la entidad
        /// </summary>
        /// <param name="entityToDelete">Entidad a eliminar</param>
        public virtual void Delete(TEntity entityToDelete)
        {
            _dbContext.Attach(entityToDelete);
            _dbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// Metodo generico para modificar una entidad en el contexto de trabajo
        /// </summary>
        /// <param name="entityToUpdate">La entidad a modificar</param>
        public virtual void Update(TEntity entityToUpdate)
        {
            _dbContext.SetModified(entityToUpdate);
        }

        /// <summary>
        /// Implementacion generica de un metodo para paginar
        /// </summary>
        /// <typeparam name="TKey">Clave para el orden</typeparam>
        /// <param name="pageIndex">Indice de la pagina a recuperar</param>/// 
        /// <param name="pageCount">Numero de entidades a recuperar</param>
        /// <param name="orderByExpression">Order expression</param>
        /// <param name="ascending">Orden ascendente o descendente</param>
        /// <returns>Listado de todas las entidades qeu cumplan los requisitos</returns>
        public IEnumerable<TEntity> GetPagedElements<TKey>(int pageIndex, int pageCount, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending = true)
        {
            if (pageIndex < 1) { pageIndex = 1; }

            if (orderByExpression == null)
                throw new ArgumentNullException();

            return (ascending)
                            ?
                        _dbSet.OrderBy(orderByExpression)
                            .Skip((pageIndex - 1) * pageCount)
                            .Take(pageCount)
                            .ToList()
                            :
                        _dbSet.OrderByDescending(orderByExpression)
                            .Skip((pageIndex - 1) * pageCount)
                            .Take(pageCount)
                            .ToList();
        }

        /// <summary>
        /// Implementacion generica de un metodo para paginar
        /// </summary>
        /// <typeparam name="TKey">Clave para el orden</typeparam>
        /// <param name="pageIndex">Indice de la pagina a recuperar</param>/// 
        /// <param name="pageCount">Numero de entidades a recuperar</param>
        /// <param name="orderByExpression">La expresion para establecer el orden</param>
        /// <param name="ascending">Si el orden es ascendente o descendente</param>
        /// <param name="includeProperties">Includes</param>
        /// <returns>Listado con todas las entidades que cumplan los criterios</returns>        
        public IEnumerable<TEntity> GetPagedElements<TKey>(int pageIndex, int pageCount, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending = true, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (pageIndex < 1) { pageIndex = 1; }

            if (orderByExpression == null)
                throw new ArgumentNullException();

            return (ascending)
                            ?
                        query.OrderBy(orderByExpression)
                            .Skip((pageIndex - 1) * pageCount)
                            .Take(pageCount)
                            .ToList()
                            :
                        query.OrderByDescending(orderByExpression)
                            .Skip((pageIndex - 1) * pageCount)
                            .Take(pageCount)
                            .ToList();
        }

        /// <summary>
        /// Ejecutar una query en la base de datos
        /// </summary>
        /// <param name="sqlQuery">La Query</param>
        /// <param name="parameters">The parameters</param>
        /// <returns>Listado de entidades que recupera la query</returns>
        public IEnumerable<TEntity> GetFromDatabaseWithQuery(string sqlQuery, params object[] parameters)
        {
            return _dbContext.ExecuteQuery<TEntity>(sqlQuery, parameters);
        }

        /// <summary>
        /// Ejecutar un command en la base de datos 
        /// </summary>
        /// <param name="sqlCommand">La query</param>
        /// <param name="parameters">Los parametros</param>
        /// <returns>El sql code que devuelve la query</returns>
        public int ExecuteInDatabaseByQuery(string sqlCommand, params object[] parameters)
        {
            return _dbContext.ExecuteCommand(sqlCommand, parameters);
        }
    }
}