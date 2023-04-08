using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NET104_PH27305_ASSIGNMENT.Models;

namespace NET104_PH27305_ASSIGNMENT.Configurations;

public class BillConfiguration : IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.HasKey(p => new { p.Id });
        builder.Property(p => p.Status).HasColumnType("int").IsRequired();
        builder.HasOne(p => p.User).WithMany(p => p.Bills).HasForeignKey(p => p.UserId);
    }
}

