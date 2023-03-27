namespace SellerProduct.Models;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
    public virtual List<User> Users { get; set; }


}
