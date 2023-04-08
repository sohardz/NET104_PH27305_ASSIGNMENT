using NET104_PH27305_ASSIGNMENT.IServices;
using NET104_PH27305_ASSIGNMENT.Models;

namespace NET104_PH27305_ASSIGNMENT.Services;

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
        {
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
        {
            var product = context.Products.Find(p.Id);
            product.AvailableQuantity = p.AvailableQuantity;
            product.Price = p.Price;
            product.Status = p.Status;
            product.Name = p.Name;
            product.ImageDirection = p.ImageDirection;
            product.Description = p.Description;
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
