using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NET104_PH27305_ASSIGNMENT.Models;
namespace NET104_PH27305_ASSIGNMENT.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasColumnType("nvarchar(256)");
        builder.Property(x => x.Password).HasColumnType("varchar(256)");
        builder.HasOne(p => p.Role).WithMany(p => p.Users).HasForeignKey(p => p.RoleId);
        builder.HasAlternateKey(p => p.Name); // Unique
    }
}
