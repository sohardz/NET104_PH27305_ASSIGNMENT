using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace NET104_PH27305_ASSIGNMENT.Models;

public class ShopDbContext : DbContext
{
    public ShopDbContext() { }
    public ShopDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Product> Products { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<BillDetails> BillDetails { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartDetail> CartDetails { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-P5GSVI1;Initial Catalog=IT17304_HocLai;Integrated Security=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
