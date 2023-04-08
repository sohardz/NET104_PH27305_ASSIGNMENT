using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NET104_PH27305_ASSIGNMENT.Models;
namespace NET104_PH27305_ASSIGNMENT.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).HasColumnType("varchar(100)");
        builder.Property(p => p.Description).HasColumnType("nvarchar(1000)");
    }
}
