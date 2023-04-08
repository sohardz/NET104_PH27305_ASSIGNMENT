using NET104_PH27305_ASSIGNMENT.Models;

namespace NET104_PH27305_ASSIGNMENT.IServices;

public interface ICartDetailServices
{
    public bool Create(CartDetail p);
    public bool Update(Guid productId, Guid userId, CartDetail obj);
    public bool Delete(Guid productId, Guid userId);
    public List<CartDetail> GetAll();
    public CartDetail GetById(Guid productId, Guid userId);
    //public List<CartDetail> GetByName(string name);
}
