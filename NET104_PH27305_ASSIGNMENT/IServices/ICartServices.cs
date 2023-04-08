using NET104_PH27305_ASSIGNMENT.Models;

namespace NET104_PH27305_ASSIGNMENT.IServices;

public interface ICartServices
{
    public bool Create(Cart p);
    public bool Update(Cart p);
    public bool Delete(Guid id);
    public List<Cart> GetAll();
    public Cart GetById(Guid id);
    //public List<Cart> GetByName(string name);
}
