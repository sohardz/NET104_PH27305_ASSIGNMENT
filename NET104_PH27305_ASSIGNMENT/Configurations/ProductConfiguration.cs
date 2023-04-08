using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NET104_PH27305_ASSIGNMENT.Models;

namespace NET104_PH27305_ASSIGNMENT.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).HasColumnType("nvarchar(100)");
        builder.Property(p => p.ImageDirection).HasColumnName("varchar(500)");
        builder.Property(p => p.Supplier).IsUnicode(true).IsFixedLength().HasMaxLength(100);// nvarchar(100)
    }
}
