using NET104_PH27305_ASSIGNMENT.IServices;
using NET104_PH27305_ASSIGNMENT.Models;

namespace NET104_PH27305_ASSIGNMENT.Services;

public class CartDetailServices : ICartDetailServices
{
    ShopDbContext context;

    public CartDetailServices()
    {
        context = new ShopDbContext();
    }

    public bool Create(CartDetail p)
    {
        try
        {
            context.CartDetails.Add(p);
            context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Delete(Guid productId, Guid userId)
    {
        try
        {
            var list = context.CartDetails.ToList();
            var obj = list.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);

            context.Remove(obj);
            context.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public List<CartDetail> GetAll()
    {
        return context.CartDetails.ToList();
    }

    public CartDetail GetById(Guid productId, Guid userId)
    {
        var list = context.CartDetails.ToList();
        var obj = list.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);
        if (obj == null)
        {
            return new CartDetail();
        }
        return obj;
    }

    //public List<CartDetail> GetByName(string name)
    //{
    //    return context.CartDetails.Where(p => p.CartDetailName.Contains(name)).ToList();
    //}

    public bool Update(Guid productId, Guid userId, CartDetail obj)
    {
        try
        {
            var listObj = context.CartDetails.ToList();
            var objForUpdate = listObj.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);

            objForUpdate.Quantity = obj.Quantity;

            context.CartDetails.Update(objForUpdate);
            context.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
