using CavipetrolTestBack.Repositories.Configuration;
using CavipetrolTestBack.DTOs.Extends;
using CavipetrolTestBack.DTOs.Objects;
using CavipetrolTestBack.Infrastructure.Utils;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace CavipetrolTestBack.Repositories.Context
{
    public partial class CavipetrolDBContext : IdentityDbContext, IDBContext, IDataProtectionKeyContext
    {
        #region properties

        private readonly IHttpContextAccessor _contextAccessor;
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; } = null!;
        #endregion

        #region Constructors
        public CavipetrolDBContext(DbContextOptions<CavipetrolDBContext> options, IHttpContextAccessor contextAccessor = null) : base(options)
        {
            _contextAccessor = contextAccessor;
        }
        #endregion

        #region DBSets
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<TransactionAudit> TransactionAudit { get; set; }

        #endregion

        #region methods
        protected override void OnModelCreating(ModelBuilder builder)
        {

            var typeConfigurations = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                (type.BaseType?.IsGenericType ?? false)
                    && (type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)
                        ));

            foreach (var typeConfiguration in typeConfigurations)
            {
                var configuration = (IMappingConfiguration)Activator.CreateInstance(typeConfiguration);
                configuration.ApplyConfiguration(builder);
            }

            //OnDelete Restrict
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);
        }

        protected virtual string CreateSqlWithParameters(string sql, params object[] parameters)
        {
            if (parameters != null)
            {
                //add parameters to sql
                for (var i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
                {
                    if (!(parameters[i] is DbParameter parameter))
                        continue;

                    sql = $"{sql}{(i > 0 ? "," : string.Empty)} @{parameter.ParameterName}";

                    //whether parameter is output
                    if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output)
                        sql = $"{sql} output";
                }

            }

            return sql;
        }

        public override int SaveChanges()
        {
            var userName = _contextAccessor?.HttpContext?.User?.Identity?.Name ?? Environment.UserName;
            var userIp = _contextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";

            //DateTime now = DateTime.Now.UsingTimeZone();
            DateTime now = DateTime.Now;

            var entitiesWithBase = ChangeTracker.Entries().Where(x => x.Entity.GetType().IsSubclassOf(typeof(MasterBase)));

            foreach (var entry in entitiesWithBase)
            {
                if (entry.State == EntityState.Added)
                {
                    ((MasterBase)entry.Entity).CreatedDate = now;
                    ((MasterBase)entry.Entity).CreatedBy = userName;
                }
                else if (entry.State == EntityState.Modified)
                {
                    ((MasterBase)entry.Entity).CreatedDate =
                        entry.OriginalValues.GetValue<DateTime>("CreatedDate");
                    ((MasterBase)entry.Entity).CreatedBy =
                        entry.OriginalValues.GetValue<string>("CreatedBy");
                }
            }

            foreach (var entry in entitiesWithBase.Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                ((MasterBase)entry.Entity).UpdatedDate = now;
                ((MasterBase)entry.Entity).UpdatedBy = !string.IsNullOrEmpty(userName) ? userName : ((MasterBase)entry.Entity).UpdatedBy;
            }

            //Si hay entities nuevas, se pospone auditoria hasta conocer el id generado
            var newEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added).ToList();

            AddAudit(userName, userIp, now);
            int result = base.SaveChanges();

            if (newEntities.Any())
            {
                AddAudit(userName, userIp, now, EntityState.Added, newEntities);
                base.SaveChanges();
            }

            return result;
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var userName = _contextAccessor?.HttpContext?.User?.Identity?.Name ?? Environment.UserName;
            var userIp = _contextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";

            //DateTime now = DateTime.Now.UsingTimeZone();
            DateTime now = DateTime.Now;

            var entitiesWithBase = ChangeTracker.Entries().Where(x => x.Entity.GetType().BaseType == typeof(MasterBase));

            foreach (var entry in entitiesWithBase)
            {
                if (entry.State == EntityState.Added)
                {
                    ((MasterBase)entry.Entity).CreatedDate = now;
                    ((MasterBase)entry.Entity).CreatedBy = userName;
                }
                else if (entry.State == EntityState.Modified)
                {
                    ((MasterBase)entry.Entity).CreatedDate =
                        entry.OriginalValues.GetValue<DateTime>("CreateTime");
                    ((MasterBase)entry.Entity).CreatedBy =
                        entry.OriginalValues.GetValue<string>("CreatedBy");
                }
            }

            foreach (var entry in entitiesWithBase.Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                ((MasterBase)entry.Entity).UpdatedDate = now;
                ((MasterBase)entry.Entity).UpdatedBy = !string.IsNullOrEmpty(userName) ? userName : ((MasterBase)entry.Entity).UpdatedBy;
            }

            //Si hay entities nuevas, se pospone auditoria hasta conocer el id generado
            var newEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added).ToList();

            AddAudit(userName, userIp, now);

            var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            result.Wait();

            if (newEntities.Any())
            {
                AddAudit(userName, userIp, now, EntityState.Added, newEntities);
                base.SaveChanges();
            }

            return result;
        }

        private void AddAudit(string userName, string userIp, DateTime now, EntityState? state = null, IEnumerable<EntityEntry> entityEntries = null)
        {
            //Si hay entities nuevas, se pospone auditoria hasta conocer el id generado
            var entitiesWithChanges = ChangeTracker.Entries().Where(x => x.State != EntityState.Added && x.State != EntityState.Unchanged);


            if (entityEntries != null && state != null)
            {
                entitiesWithChanges = entityEntries;
            }

            var auditRepository = Set<TransactionAudit>();
            var auditList = new List<TransactionAudit>();
            foreach (var entry in entitiesWithChanges)
            {
                var modifiedStringProperties = string.Empty;
                if (entry.State != EntityState.Added)
                {
                    var original = entry.GetDatabaseValues();
                    if (original != null)
                    {
                        var propertyNames = entry.Metadata.GetProperties().Select(p => p.Name);
                        var modifiedProperties = propertyNames.Select(p => entry.Property(p)).Where(e => e.IsModified && (!e.CurrentValue?.Equals(original.GetValue<object>(e.Metadata.Name)) ?? original.GetValue<object>(e.Metadata.Name) != null));
                        if (modifiedProperties.Any())
                            modifiedStringProperties = string.Join("|", modifiedProperties.Select(x => x.Metadata.Name).ToArray());
                    }
                }

                string entityId = entry.Metadata.FindProperty("Id") != null ? entry.Property("Id")?.OriginalValue?.ToString() : "Unknown";
                auditList.Add(new TransactionAudit()
                {
                    Id = GuidGenerator.Create(),
                    Action = state?.ToString() ?? entry.State.ToString(),
                    Entity = entry.Entity.GetType().Name,
                    EntityId = entityId,
                    ModifiedProperties = modifiedStringProperties,
                    UserName = userName,
                    ActionDate = now
                });
            }

            if (auditList.Any())
            {
                auditRepository.AddRange(auditList);
            }
        }

        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public virtual string GenerateCreateScript()
        {
            return this.Database.GenerateCreateScript();
        }

        public virtual IQueryable<TQuery> QueryFromSql<TQuery>(string sql) where TQuery : class
        {
            return this.Set<TQuery>().FromSqlRaw(sql);
        }

        public virtual IQueryable<TEntity> EntityFromSql<TEntity>(string sql, params object[] parameters) where TEntity : class
        {
            return this.Set<TEntity>().FromSqlRaw(CreateSqlWithParameters(sql, parameters), parameters);
        }

        public virtual int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            //set specific command timeout
            var previousTimeout = this.Database.GetCommandTimeout();
            this.Database.SetCommandTimeout(timeout);

            var result = 0;
            if (!doNotEnsureTransaction)
            {
                //use with transaction
                using var transaction = this.Database.BeginTransaction();
                result = this.Database.ExecuteSqlRaw(sql, parameters);
                transaction.Commit();
            }
            else
                result = this.Database.ExecuteSqlRaw(sql, parameters);

            //return previous timeout back
            this.Database.SetCommandTimeout(previousTimeout);

            return result;
        }

        public virtual void Detach<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var entityEntry = this.Entry(entity);
            if (entityEntry == null)
                return;

            //set the entity is not being tracked by the context
            entityEntry.State = EntityState.Detached;
        }

        public List<string> GetDatabaseTableNames()
        {
            List<string> result = Model.GetEntityTypes().Select(x => x.GetTableName()).ToList();

            return result;
        }
        #endregion
    }
}
