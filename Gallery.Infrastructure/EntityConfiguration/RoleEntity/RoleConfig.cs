using Gallery.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;

namespace Gallery.Infrastructure.EntityConfiguration.RoleEntity
{
    internal class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(nameof(Role));
            builder.Property(x => x.RolePersianName).HasMaxLength(70).HasColumnType(SqlDbType.NVarChar.ToString());

        }
    }
}