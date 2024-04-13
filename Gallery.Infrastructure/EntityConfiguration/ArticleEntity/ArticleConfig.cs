using Gallery.Infrastructure.EntityConfiguration;
using Gallery.Models.Entities.ArticleEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;

namespace Gallery.Infrastructrue.EntityConfiguration.ArticleEntity
{
    internal class ArticleConfig : BaseEntityConfiguration<Article, int>
    {
        public override void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable(nameof(Article));

           
            builder.Property(x => x.Title).IsRequired().HasMaxLength(250).HasColumnType(SqlDbType.NVarChar.ToString());
            builder.Property(x => x.Picture).IsRequired().HasMaxLength(250).HasColumnType(SqlDbType.NVarChar.ToString());
            builder.Property(x => x.PictureAlt).IsRequired().HasMaxLength(250).HasColumnType(SqlDbType.NVarChar.ToString());
            builder.Property(x => x.PictureTitle).IsRequired().HasMaxLength(250).HasColumnType(SqlDbType.NVarChar.ToString());
            builder.Property(x => x.ShortDescription).IsRequired(false).HasMaxLength(400).HasColumnType(SqlDbType.NVarChar.ToString());
            builder.Property(x => x.Body).IsRequired(false).HasMaxLength(400).HasColumnType(SqlDbType.NVarChar.ToString());
        }
    }
}
