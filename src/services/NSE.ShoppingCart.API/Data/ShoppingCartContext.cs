using Microsoft.EntityFrameworkCore;
using NSE.ShoppingCart.API.Models;

namespace NSE.ShoppingCart.API.Data;

public sealed class ShoppingCartContext : DbContext
{
    public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }
    
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<CustomerCart> CustomerCarts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetProperties())
                     .Where(p => p.ClrType == typeof(string)))
        {
            property.SetColumnType("varchar(100)");
        }

        modelBuilder.Entity<Models.CustomerCart>()
            .HasIndex(c => c.CustomerId)
            .HasDatabaseName("IDX_Customer");

        modelBuilder.Entity<Models.CustomerCart>()
            .HasMany(c => c.Items)
            .WithOne(i => i.CustomerCart)
            .HasForeignKey(i => i.CustomerCartId);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShoppingCartContext).Assembly);
    }
}