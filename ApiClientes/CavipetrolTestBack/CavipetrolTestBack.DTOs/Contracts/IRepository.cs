using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CavipetrolTestBack.DTOs.Contracts
{
    public partial interface IRepository<TEntity> where TEntity : class
    {
        #region Methods
        TEntity GetById(object id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAllIncludes(params string[] includes);
        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);
        TEntity Get(Expression<Func<TEntity, bool>> where);
        void Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        void Delete(Expression<Func<TEntity, bool>> where);
        int Count(Expression<Func<TEntity, bool>> where);
        #endregion

        #region properties
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }
        #endregion
    }
}
