using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Customers.API.Models;

namespace NSE.Customers.API.Data.Mappings;

public class AddressMapping : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(a => a.Id);
        
        builder.Property(a => a.Street)
            .HasColumnType("varchar(200)")
            .IsRequired();
        
        builder.Property(a => a.Number)
            .HasColumnType("varchar(50)")
            .IsRequired();
        
        builder.Property(a => a.PostalCode)
            .HasColumnType("varchar(20)")
            .IsRequired();
        
        builder.Property(a => a.AdditionalInfo)
            .HasColumnType("varchar(250)")
            .IsRequired();
        
        builder.Property(a => a.Neighborhood)
            .HasColumnType("varchar(100)")
            .IsRequired();
        
        builder.Property(a => a.City)
            .HasColumnType("varchar(100)")
            .IsRequired();
        
        builder.Property(a => a.State)
            .HasColumnType("varchar(50)")
            .IsRequired();
        
        builder.ToTable("Addresses");
    }
}