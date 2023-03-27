using SellerProduct.IServices;
using SellerProduct.Models;

namespace SellerProduct.Services;

public class RoleServices : IRoleServices
{
    ShopDbContext context;

    public RoleServices()
    {
        context = new ShopDbContext();
    }

    public bool Create(Role p)
    {
        try
        {
            context.Roles.Add(p);
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
            var Role = context.Roles.Find(id);
            context.Roles.Remove(Role);
            context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public List<Role> GetAll()
    {
        return context.Roles.ToList();
    }

    public Role GetById(Guid id)
    {
        return context.Roles.FirstOrDefault(p => p.Id == id);
        // return context.Roles.SingleOrDefault(p => p.Id == id);
    }

    public List<Role> GetByName(string name)
    {
        return context.Roles.Where(p => p.Name.Contains(name)).ToList();
    }

    public bool Update(Role p)
    {
        try
        {// Find(id) chỉ dùng được khi id là khóa chính
            var role = context.Roles.Find(p.Id);
            role.Name = p.Name;
            role.Description = p.Description;
            role.Status = p.Status;
            // Có thể sửa thêm thuộc tính
            context.Roles.Update(role);
            context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
