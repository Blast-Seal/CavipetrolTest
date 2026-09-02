using CavipetrolTestBack.DTOs.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CavipetrolTestBack.Repositories.Configuration
{
    internal partial class EFRepository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
        #region propiedades

        protected readonly IDBContext _context;

        private DbSet<TEntity> _entities;
        public virtual IQueryable<TEntity> Table => Entities;
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<TEntity>();

                return _entities;
            }
        }

        #endregion
        #region constructor
        public EFRepository(IDBFactory dbFactory)
        {
            _context = dbFactory.CreateDbContext(null);
        }
        #endregion

        #region Methods
        public virtual TEntity GetById(object id)
        {
            return Entities.Find(id);
        }
        public virtual void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Add(entity);
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            Entities.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));


            ((DbContext)_context).Entry(entity).State = EntityState.Modified;
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            foreach (TEntity entity in entities)
            {
                this.Entities.Attach(entity);
                ((DbContext)_context).Entry(entity).State = EntityState.Modified;
            }
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Remove(entity);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            Entities.RemoveRange(entities);
        }

        public void Delete(Expression<Func<TEntity, bool>> where)
        {

            IEnumerable<TEntity> objects = Entities.Where(where).AsEnumerable();
            foreach (TEntity obj in objects)
            {
                Entities.Attach(obj);
                Entities.Remove(obj);
            }
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Entities;
        }

        public virtual IEnumerable<TEntity> GetAllIncludes(params string[] includes)
        {
            IQueryable<TEntity> query = Entities;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.ToList();
        }

        public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return Entities.Where(where);
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return Entities.Where(where).FirstOrDefault();
        }

        public int Count(Expression<Func<TEntity, bool>> where)
        {
            return this.Entities.Where(where).Count();
        }

        public void Dispose()
        {
            (_context as IDisposable)?.Dispose();
        }
        #endregion
    }
}
