using Microsoft.EntityFrameworkCore;
using NSE.Core.Data;
using NSE.Core.Mediator;
using NSE.Customers.API.Extensions;
using NSE.Customers.API.Models;

namespace NSE.Customers.API.Data;

public sealed class CustomerContext : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;
    
    public CustomerContext(DbContextOptions<CustomerContext> options, IMediatorHandler mediatorHandler) : base(options)
    {
        _mediatorHandler = mediatorHandler;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }
    
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetProperties())
                     .Where(p => p.ClrType == typeof(string)))
        {
            property.SetColumnType("varchar(100)");
        }

        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerContext).Assembly);
    }

    public async Task<bool> Commit()
    {
        var isSuccess = await SaveChangesAsync() > 0;
        
        if (isSuccess) await _mediatorHandler.DispatchDomainEventsAsync(this);
        
        return isSuccess;
    }
}