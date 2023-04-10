using NET104_PH27305_ASSIGNMENT.Models;

namespace NET104_PH27305_ASSIGNMENT.IServices;

public interface IUserServices
{
    public bool Create(User p);
    public bool Update(User p);
    public bool Delete(Guid id);
    public List<User> GetAll();
    public User GetById(Guid id);
    public List<User> GetByName(string name);
    public User Login(string email, string password);
}
