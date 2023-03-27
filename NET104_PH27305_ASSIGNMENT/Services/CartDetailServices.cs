using SellerProduct.IServices;
using SellerProduct.Models;

namespace SellerProduct.Services;

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

    public bool Delete(Guid id)
    {
        try
        {// Find(id) chỉ dùng được khi id là khóa chính
            var CartDetail = context.CartDetails.Find(id);
            context.CartDetails.Remove(CartDetail);
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

    public CartDetail GetById(Guid id)
    {
        return context.CartDetails.FirstOrDefault(p => p.Id == id);
        // return context.CartDetails.SingleOrDefault(p => p.Id == id);
    }

    //public List<CartDetail> GetByName(string name)
    //{
    //    return context.CartDetails.Where(p => p.CartDetailName.Contains(name)).ToList();
    //}

    public bool Update(CartDetail p)
    {
        try
        {// Find(id) chỉ dùng được khi id là khóa chính
            var CartDetail = context.CartDetails.Find(p.Id);
            CartDetail.ProductId = p.ProductId;
            CartDetail.Quantity = p.Quantity;
            // Có thể sửa thêm thuộc tính
            context.CartDetails.Update(CartDetail);
            context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
