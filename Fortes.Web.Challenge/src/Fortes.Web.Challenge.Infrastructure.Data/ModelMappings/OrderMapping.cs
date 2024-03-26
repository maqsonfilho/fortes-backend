using Fortes.Web.Challenge.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Fortes.Web.Challenge.Infrastructure.Data.ModelMappings;

public class OrderMapping : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders")
                .HasKey(p => p.Id);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.Code)
            .IsRequired();

        builder.Property(p => p.Date)
            .IsRequired();

        builder.Property(p => p.Amount)
            .IsRequired();

        builder.Property(p => p.TotalValue)
            .IsRequired();

        builder.HasOne(o => o.Supplier)
            .WithMany(s => s.Orders)
            .HasForeignKey(o => o.SupplierId);

        builder.HasOne(o => o.Product)
            .WithMany(p => p.Orders)
            .HasForeignKey(o => o.ProductId);
    }
}
