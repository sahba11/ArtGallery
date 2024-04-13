using Gallery.Models.BaseEntityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gallery.Infrastructure.EntityConfiguration
{
    /// <summary>
    /// base entity class for table config
    /// </summary>
    /// <typeparam name="T">object type</typeparam>
    /// <typeparam name="KeyTypeId">key value type</typeparam>
    internal class BaseEntityConfiguration<T, KeyTypeId> : IEntityTypeConfiguration<T> where T : BaseEntity<KeyTypeId> where KeyTypeId : struct
    {
        /// <summary>
        /// if the type of the table key is GUID or somethings like this, we nust set the GeneratedValueForKey property to false
        /// </summary>
        protected bool GeneratedValueForKey { get; set; } = true;
        protected bool UseForTraceable { get; set; } = false;
        protected bool RequiredTraceable { get; set; } = false;

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
            if (GeneratedValueForKey == true)
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
            else
                builder.Property(x => x.Id).ValueGeneratedNever();

            if (UseForTraceable == false)
            {
                builder.Ignore(x => x.CreateDateTime);
                builder.Ignore(x => x.UpdateDateTime);
                //builder.Ignore(x => x.CreateUser);
                //builder.Ignore(x => x.LastUpdateUser);
                builder.Ignore(x => x.CreateUserId);
                builder.Ignore(x => x.LastUpdateUserId);
            }
            else
            {
                builder.Property(x => x.CreateDateTime);
                builder.Property(x => x.UpdateDateTime);
                //builder.HasOne(x => x.CreateUser).WithMany().HasForeignKey(x => x.CreateUserId).IsRequired(RequiredTraceable).OnDelete(DeleteBehavior.NoAction);
                //builder.HasOne(x => x.LastUpdateUser).WithMany().HasForeignKey(x => x.LastUpdateUserId).IsRequired(RequiredTraceable).OnDelete(DeleteBehavior.NoAction);
            }
        }
    }
}