using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NET104_PH27305_ASSIGNMENT.Models;
namespace NET104_PH27305_ASSIGNMENT.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(p => p.UserId);
        builder.Property(p => p.Description).HasColumnType("nvarchar(100)");
    }
}
