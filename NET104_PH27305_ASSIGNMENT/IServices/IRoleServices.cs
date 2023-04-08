using NET104_PH27305_ASSIGNMENT.Models;

namespace NET104_PH27305_ASSIGNMENT.IServices;

public interface IRoleServices
{
    public bool Create(Role p);
    public bool Update(Role p);
    public bool Delete(Guid id);
    public List<Role> GetAll();
    public Role GetById(Guid id);
    public List<Role> GetByName(string name);
}
