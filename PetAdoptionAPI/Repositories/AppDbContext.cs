using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using PetAdoptionAPI.Constants;
using PetAdoptionAPI.Entities;

namespace PetAdoptionAPI.Repositories;

public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Pets => Set<Product>();
    public DbSet<Category> Category => Set<Category>();
    public DbSet<Purchase> Purchases => Set<Purchase>();
    public DbSet<PurchaseDetail> PurchaseDetails => Set<PurchaseDetail>();

    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // set NotNull for property
        modelBuilder.Entity<Product>()
        .Property(p => p.ProductName).IsRequired();

        modelBuilder.Entity<Category>()
        .Property(c => c.CategoryName).IsRequired();

        // relationship between Product and Category
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<Account>()
            .Property(e => e.Role)
            .HasConversion(
                v => v.ToString(),
                v => (Role)Enum.Parse(typeof(Role), v));

        base.OnModelCreating(modelBuilder);
    }
}