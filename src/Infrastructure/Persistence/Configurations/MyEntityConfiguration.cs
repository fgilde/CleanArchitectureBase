using CleanArchitectureBase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureBase.Infrastructure.Persistence.Configurations
{
    class MyEntityConfiguration : IEntityTypeConfiguration<MyEntity>
    {
        public void Configure(EntityTypeBuilder<MyEntity> builder)
        {
            builder.Ignore(e => e.DomainEvents);
            builder.ToTable("MyEntity");
            builder.HasKey(e => e.Id);
        }

    }
}
