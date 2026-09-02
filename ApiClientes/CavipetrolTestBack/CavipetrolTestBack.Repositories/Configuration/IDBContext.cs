using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CavipetrolTestBack.Repositories.Configuration
{
    public partial interface IDBContext
    {
        #region Methods
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancelationToken = default);
        string GenerateCreateScript();
        IQueryable<TQuery> QueryFromSql<TQuery>(string sql) where TQuery : class;
        IQueryable<TEntity> EntityFromSql<TEntity>(string sql, params object[] parameters) where TEntity : class;
        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);
        void Detach<TEntity>(TEntity entity) where TEntity : class;
        List<string> GetDatabaseTableNames();
        #endregion
    }
}
