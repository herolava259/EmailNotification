using Cart.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cart.Infrastructure.Configurations;

public abstract class AbstractEntityBaseFluentConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.CreatedDate).IsRequired();
        builder.Property(c => c.UpdatedDate).IsRequired()
                                            ;
        builder.HasIndex(c => c.Id);
        builder.HasIndex(c => c.CreatedDate);

        builder.HasQueryFilter(c => !c.IsDeleted);
        
    }
}
