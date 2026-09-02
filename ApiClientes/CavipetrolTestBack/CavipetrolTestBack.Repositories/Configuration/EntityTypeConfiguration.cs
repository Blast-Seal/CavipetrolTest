using CavipetrolTestBack.DTOs.Extends;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CavipetrolTestBack.Repositories.Configuration
{
    public partial class EntityTypeConfiguration<TEntity> : IMappingConfiguration, IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        #region methods
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            //add custom configuration
            if (builder.GetType().GenericTypeArguments.Select(g => g.BaseType).Contains(typeof(MasterBase)))
            {
                builder.HasBaseType((Type)null);
                builder.Property(x => (x as MasterBase).CreatedBy).IsRequired().HasMaxLength(50).HasDefaultValue("Default");
                builder.Property(x => (x as MasterBase).UpdatedBy).IsRequired().HasMaxLength(50).HasDefaultValue("Default");
                builder.Property(x => (x as MasterBase).CreatedDate).IsRequired().HasDefaultValueSql("getdate()");
                builder.Property(x => (x as MasterBase).UpdatedDate).IsRequired().HasDefaultValueSql("getdate()");
            }

            //add custom configuration
            if (builder.GetType().GenericTypeArguments.Select(g => g.BaseType).Contains(typeof(ParamBase)))
            {
                builder.HasBaseType((Type)null);
                builder.Property(x => (x as ParamBase).CreatedBy).IsRequired().HasMaxLength(50).HasDefaultValue("Default");
                builder.Property(x => (x as ParamBase).UpdatedBy).IsRequired().HasMaxLength(50).HasDefaultValue("Default");
                builder.Property(x => (x as ParamBase).CreatedDate).IsRequired().HasDefaultValueSql("getdate()");
                builder.Property(x => (x as ParamBase).UpdatedDate).IsRequired().HasDefaultValueSql("getdate()");
            }
        }

        public virtual void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }
        #endregion
    }
}
