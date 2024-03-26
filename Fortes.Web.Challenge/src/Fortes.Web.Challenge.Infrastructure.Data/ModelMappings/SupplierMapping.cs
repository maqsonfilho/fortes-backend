using Fortes.Web.Challenge.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Fortes.Web.Challenge.Infrastructure.Data.ModelMappings;

public class SupplierMapping : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("Suppliers")
                .HasKey(p => p.Id);

        builder.Property(p => p.District)
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.ContactEmail)
            .IsRequired();

        builder.Property(p => p.ContactName)
            .IsRequired();

        builder.HasMany(o => o.Orders)
            .WithOne(o => o.Supplier)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
