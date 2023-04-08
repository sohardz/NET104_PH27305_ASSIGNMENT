using SellerProduct.Models;

namespace SellerProduct.IServices;

public interface ICartDetailServices
{
    public bool Create(CartDetail p);
    public bool Update(Guid productId, Guid userId, CartDetail obj);
    public bool Delete(Guid productId, Guid userId);
    public List<CartDetail> GetAll();
    public CartDetail GetById(Guid productId, Guid userId);
    //public List<CartDetail> GetByName(string name);
}
