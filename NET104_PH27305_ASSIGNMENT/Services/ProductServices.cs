using SellerProduct.IServices;
using SellerProduct.Models;

namespace SellerProduct.Services;

public class ProductServices : IProductServices
{
    ShopDbContext context;

    public ProductServices()
    {
        context = new ShopDbContext();
    }

    public bool Create(Product p)
    {
        try
        {
            context.Products.Add(p);
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
            var product = context.Products.Find(id);
            context.Products.Remove(product);
            context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public List<Product> GetAll()
    {
        return context.Products.ToList();
    }

    public Product GetById(Guid id)
    {
        return context.Products.FirstOrDefault(p => p.Id == id);
        // return context.Products.SingleOrDefault(p => p.Id == id);
    }

    public List<Product> GetByName(string name)
    {
        return context.Products.Where(p => p.Name.Contains(name)).ToList();
    }

    public bool Update(Product p)
    {
        try
        {// Find(id) chỉ dùng được khi id là khóa chính
            var product = context.Products.Find(p.Id);
            product.AvailableQuantity = p.AvailableQuantity;
            product.Price = p.Price;
            product.Status = p.Status;
            product.Name = p.Name;
            product.Description = p.Description;
            // Có thể sửa thêm thuộc tính
            context.Products.Update(product);
            context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
