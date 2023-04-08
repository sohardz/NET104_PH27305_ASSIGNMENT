using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NET104_PH27305_ASSIGNMENT.Models;
namespace NET104_PH27305_ASSIGNMENT.Configurations;

public class CartDetailConfiguration : IEntityTypeConfiguration<CartDetail>
{
    public void Configure(EntityTypeBuilder<CartDetail> builder)
    {
        builder.HasKey(x => new { x.Id });
        builder.HasOne(p => p.Cart).WithMany(p => p.CartDetails).HasForeignKey(p => p.UserId);
        builder.HasOne(p => p.Product).WithMany(p => p.CartDetails).HasForeignKey(p => p.ProductId);
    }
}
