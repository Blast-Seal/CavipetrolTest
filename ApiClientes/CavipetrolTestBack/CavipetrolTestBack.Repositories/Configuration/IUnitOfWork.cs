using System;
using System.Collections.Generic;
using System.Text;

namespace CavipetrolTestBack.Repositories.Configuration
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync(bool acceptAllChangesOnSuccess, CancellationToken cancelationToken = default);
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDBContext _dbContext;

        public UnitOfWork(IDBFactory dbFactory)
        {
            _dbContext = dbFactory.CreateDbContext(null);
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public async Task CommitAsync(bool acceptAllChangesOnSuccess, CancellationToken cancelationToken = default)
        {
            await _dbContext.SaveChangesAsync(acceptAllChangesOnSuccess, cancelationToken);
        }
    }
}
