using CleanArchitectureBase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureBase.Infrastructure.Persistence.Configurations
{
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Ignore(e => e.DomainEvents);       
            builder.ToTable("Product", "Products"); // Table, Database-Schema
            //builder.HasKey(e => e.ID); // Hier bin ich mir nicht so sicher, ob das noch benötigt wird.        
        }

    }
}
