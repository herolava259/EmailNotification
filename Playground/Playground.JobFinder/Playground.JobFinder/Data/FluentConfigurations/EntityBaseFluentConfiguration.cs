using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playground.JobFinder.Bases;

namespace Playground.JobFinder.Data.FluentConfigurations;

public abstract class EntityBaseFluentConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : EntityBase
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.CreatedAt).IsRequired();
        builder.Property(c => c.UpdatedAt).IsRequired()
                                            ;
        builder.HasIndex(c => c.Id);
        builder.HasIndex(c => c.CreatedAt);

        builder.HasQueryFilter(c => !c.IsDeleted);

    }
}
