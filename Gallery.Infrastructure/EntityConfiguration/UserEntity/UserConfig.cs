using Gallery.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;

namespace Gallery.Infrastructure.EntityConfiguration.UserEntity
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreateUserId).IsRequired(false);
            builder.Property(x => x.Email).IsRequired(false);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100).HasColumnType(SqlDbType.NVarChar.ToString());
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(150).HasColumnType(SqlDbType.NVarChar.ToString());
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(100).HasColumnType(SqlDbType.VarChar.ToString());
            builder.Property(x => x.BirthDate).IsRequired(false);
        }
    }
}
