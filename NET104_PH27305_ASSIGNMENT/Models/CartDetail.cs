namespace NET104_PH27305_ASSIGNMENT.Models;

public class CartDetail
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public virtual Cart Cart { get; set; }
    public virtual Product Product { get; set; }

}
