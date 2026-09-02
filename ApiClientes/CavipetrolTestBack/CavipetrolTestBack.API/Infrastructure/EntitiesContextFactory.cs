using CavipetrolTestBack.Repositories.Configuration;
using CavipetrolTestBack.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace CavipetrolTestBack.API.Infrastructure
{
    public class EntitiesContextFactory : IDBFactory
    {
        private readonly IConfiguration Configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        public EntitiesContextFactory()
        {

        }

        public EntitiesContextFactory(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            Configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        private CavipetrolDBContext _entities;

        public CavipetrolDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CavipetrolDBContext>();
            if (Configuration != null)
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultDBConnection"));
            else
                optionsBuilder.UseSqlServer("MIGRATION_ONLY_DONT_USE_ITS_FAKE!");


            if (_entities == null)
                _entities = new CavipetrolDBContext(optionsBuilder.Options, _contextAccessor);

            return _entities;
        }
    }
}
