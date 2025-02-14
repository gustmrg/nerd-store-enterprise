using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Core.DomainObjects;

namespace NSE.Customers.API.Data.Mappings;

public class CustomerMapping : IEntityTypeConfiguration<Models.Customer>
{
    public void Configure(EntityTypeBuilder<Models.Customer> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Name)
            .HasColumnType("varchar(200)")
            .IsRequired();
        
        builder.OwnsOne(c => c.DocumentNumber, x => 
            x.Property(c => c.Number)
                .IsRequired()
                .HasMaxLength(11)
                .HasColumnName("DocumentNumber")
                .HasColumnType($"varchar({Cpf.MaxLength})"));
        
        builder.OwnsOne(c => c.Email, x => 
            x.Property(e => e.EmailAddress)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType($"varchar({Email.MaxLength})"));

        builder.HasOne(c => c.Address)
            .WithOne(a => a.Customer);
        
        builder.ToTable("Customers");
    }
}