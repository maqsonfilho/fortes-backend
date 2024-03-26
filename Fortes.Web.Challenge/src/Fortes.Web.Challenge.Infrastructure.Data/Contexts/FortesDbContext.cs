using Fortes.Web.Challenge.Domain.Models;
using Fortes.Web.Challenge.Infrastructure.Data.ModelMappings;
using Microsoft.EntityFrameworkCore;

namespace Fortes.Web.Challenge.Infrastructure.Data.Contexts;

public class FortesDbContext : DbContext
{
    public FortesDbContext(DbContextOptions<FortesDbContext> options) : base(options) { }

    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductMapping());
        modelBuilder.ApplyConfiguration(new SupplierMapping());
        modelBuilder.ApplyConfiguration(new OrderMapping());
    }
}
