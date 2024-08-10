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
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Service> Services => Set<Service>();

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

        // relationship between Account and Customer => 1 Account can have 1 Customer
        modelBuilder.Entity<Customer>()
        .HasOne(c => c.Account)
        .WithOne(a => a.Customer)
        .HasForeignKey<Customer>(c => c.AccountId);

        // relationship between Account and Store => 1 Account can have 1 Store
        modelBuilder.Entity<Store>()
        .HasOne(s => s.Account)
        .WithOne(a => a.Store)
        .HasForeignKey<Store>(s => s.AccountId);

        // relationship between Product and Category => 1 Category can have many Products
        modelBuilder.Entity<Product>()
        .HasOne(p => p.Category)
        .WithMany(c => c.Products)
        .HasForeignKey(p => p.CategoryId);

        // relationship between Customer and Purchase => 1 Customer can have many Purchases
        modelBuilder.Entity<Purchase>()
        .HasOne(p => p.Customer)
        .WithMany(c => c.Purchases)
        .HasForeignKey(p => p.CustomerId);

        // relationship between Purchase and Purchase Detail => 1 Purchase can have many Purchase Details
        modelBuilder.Entity<PurchaseDetail>()
        .HasOne(pd => pd.Purchase)
        .WithMany(p => p.PurchaseDetails)
        .HasForeignKey(pd => pd.PurchaseId);

        // relationship between Product And Store  => 1 Store can have many Products
        modelBuilder.Entity<Product>()
        .HasOne(p => p.Store)
        .WithMany(s => s.Products)
        .HasForeignKey(p => p.StoreId);

        // relationship between Service And Purchase => 1 Service can have many Purchases
        modelBuilder.Entity<Service>()
        .HasMany(s => s.Purchases)
        .WithOne(p => p.Service)
        .HasForeignKey(p => p.ServiceId);

        // convert enum to string
        modelBuilder.Entity<Account>()
            .Property(e => e.Role)
            .HasConversion(
                v => v.ToString(),
                v => (Role)Enum.Parse(typeof(Role), v));

        modelBuilder.Entity<Purchase>()
            .Property(e => e.TransType)
            .HasConversion(
                v => v.ToString(),
                v => (TransType)Enum.Parse(typeof(TransType), v));

        base.OnModelCreating(modelBuilder);
    }
}