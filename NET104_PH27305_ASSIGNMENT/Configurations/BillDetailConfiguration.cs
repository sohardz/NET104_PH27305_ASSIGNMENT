using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SellerProduct.Models;
namespace SellerProduct.Configurations;

public class BillDetailConfiguration : IEntityTypeConfiguration<BillDetails>
{
    public void Configure(EntityTypeBuilder<BillDetails> builder)
    {
        builder.HasKey(x => new { x.Id });
        builder.Property(p => p.Quantity).IsRequired().HasColumnType("int");
        builder.HasOne(p => p.Bill).WithMany(p => p.BillDetails).HasForeignKey(p => p.BillId);
        builder.HasOne(p => p.Product).WithMany(p => p.BillDetails).HasForeignKey(p => p.ProductId);
    }
}
