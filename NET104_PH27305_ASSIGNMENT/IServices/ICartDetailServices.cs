using SellerProduct.Models;

namespace SellerProduct.IServices;

public interface ICartDetailServices
{
    public bool Create(CartDetail p);
    public bool Update(CartDetail p);
    public bool Delete(Guid id);
    public List<CartDetail> GetAll();
    public CartDetail GetById(Guid id);
    //public List<CartDetail> GetByName(string name);
}
