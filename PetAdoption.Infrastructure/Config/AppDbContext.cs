using Microsoft.EntityFrameworkCore;
using PetAdoption.Core.Constants;
using PetAdoption.Core.Entities;

namespace PetAdoption.Infrastructure.Config;

public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Pets => Set<Product>();
    public DbSet<Category> Category => Set<Category>();
    public DbSet<Order> Purchases => Set<Order>();
    public DbSet<OrderDetail> PurchaseDetails => Set<OrderDetail>();
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Review> Reviews => Set<Review>();

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

        // relationship between Customer and Order => 1 Customer can have many Orders
        modelBuilder.Entity<Order>()
        .HasOne(p => p.Customer)
        .WithMany(c => c.Orders)
        .HasForeignKey(p => p.CustomerId);

        // relationship between Order and Order Detail => 1 Order can have many Order Details
        modelBuilder.Entity<OrderDetail>()
        .HasOne(pd => pd.Order)
        .WithMany(p => p.OrderDetails)
        .HasForeignKey(pd => pd.OrderId)
        .OnDelete(DeleteBehavior.Restrict);

        // relationship between Product And Store  => 1 Store can have many Products
        modelBuilder.Entity<Product>()
        .HasOne(p => p.Store)
        .WithMany(s => s.Products)
        .HasForeignKey(p => p.StoreId);
        
        // relationship between Review and Product => 1 Product can have many Reviews
        modelBuilder.Entity<Review>()
        .HasOne(r => r.Product)
        .WithMany(p => p.Reviews)
        .HasForeignKey(r => r.ProductId)
        .OnDelete(DeleteBehavior.Restrict);

        // relationship between Review and Customer => 1 Customer can have many Reviews
        modelBuilder.Entity<Review>()
        .HasOne(r => r.Customer)
        .WithMany(c => c.Reviews)
        .HasForeignKey(r => r.CustomerId)
        .OnDelete(DeleteBehavior.Restrict);

        // convert enum to string
        modelBuilder.Entity<Account>()
            .Property(e => e.Role)
            .HasConversion(
                v => v.ToString(),
                v => (Role)Enum.Parse(typeof(Role), v));

        base.OnModelCreating(modelBuilder);
    }
}