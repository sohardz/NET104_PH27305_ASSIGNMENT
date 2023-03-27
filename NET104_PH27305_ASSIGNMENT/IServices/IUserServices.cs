using SellerProduct.Models;

namespace SellerProduct.IServices;

public interface IUserServices
{
    public bool Create(User p);
    public bool Update(User p);
    public bool Delete(Guid id);
    public List<User> GetAll();
    public User GetById(Guid id);
    public List<User> GetByName(string name);
}
