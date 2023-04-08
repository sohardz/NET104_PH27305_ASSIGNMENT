using NET104_PH27305_ASSIGNMENT.IServices;
using NET104_PH27305_ASSIGNMENT.Models;

namespace NET104_PH27305_ASSIGNMENT.Services;

public class CartServices : ICartServices
{
    ShopDbContext context;

    public CartServices()
    {
        context = new ShopDbContext();
    }

    public bool Create(Cart p)
    {
        try
        {
            context.Carts.Add(p);
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
            var Cart = context.Carts.Find(id);
            context.Carts.Remove(Cart);
            context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public List<Cart> GetAll()
    {
        return context.Carts.ToList();
    }

    public Cart GetById(Guid id)
    {
        return context.Carts.FirstOrDefault(p => p.UserId == id);
        // return context.Carts.SingleOrDefault(p => p.Id == id);
    }

    //public List<Cart> GetByName(string name)
    //{
    //    return context.Carts.Where(p => p.CartName.Contains(name)).ToList();
    //}

    public bool Update(Cart p)
    {
        try
        {// Find(id) chỉ dùng được khi id là khóa chính
            var Cart = context.Carts.Find(p.UserId);
            Cart.Description = p.Description;
            // Có thể sửa thêm thuộc tính
            context.Carts.Update(Cart);
            context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
