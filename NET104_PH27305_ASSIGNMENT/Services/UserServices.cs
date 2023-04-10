using NET104_PH27305_ASSIGNMENT.IServices;
using NET104_PH27305_ASSIGNMENT.Models;

namespace NET104_PH27305_ASSIGNMENT.Services;

public class UserServices : IUserServices
{
    ShopDbContext context;

    public UserServices()
    {
        context = new ShopDbContext();
    }

    public bool Create(User p)
    {
        try
        {
            context.Users.Add(p);
            context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Delete(Guid id)
    {
        try
        {// Find(id) chỉ dùng được khi id là khóa chính
            var user = context.Users.Find(id);
            context.Users.Remove(user);
            context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public List<User> GetAll()
    {
        return context.Users.ToList();
    }

    public User GetById(Guid id)
    {
        return context.Users.FirstOrDefault(p => p.Id == id);
        // return context.Users.SingleOrDefault(p => p.Id == id);
    }

    public List<User> GetByName(string name)
    {
        return context.Users.Where(p => p.Name.Contains(name)).ToList();
    }

    public bool Update(User p)
    {
        try
        {// Find(id) chỉ dùng được khi id là khóa chính
            var user = context.Users.Find(p.Id);
            user.Name = p.Name;
            user.RoleId = p.RoleId;
            user.Password = p.Password;
            user.Status = p.Status;
            // Có thể sửa thêm thuộc tính
            context.Users.Update(user);
            context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public User Login(string email, string password)
    {
        return context.Users.FirstOrDefault(c => c.Email == email && c.Password == password);
    }
}
