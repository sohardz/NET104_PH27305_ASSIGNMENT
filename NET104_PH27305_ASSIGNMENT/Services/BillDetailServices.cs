using SellerProduct.IServices;
using SellerProduct.Models;

namespace SellerProduct.Services;

public class BillDetailServices : IBillDetailServices
{
    ShopDbContext context;

    public BillDetailServices()
    {
        context = new ShopDbContext();
    }

    public bool Create(BillDetails p)
    {
        try
        {
            context.BillDetails.Add(p);
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
            var BillDetails = context.BillDetails.Find(id);
            context.BillDetails.Remove(BillDetails);
            context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public List<BillDetails> GetAll()
    {
        return context.BillDetails.ToList();
    }

    public BillDetails GetById(Guid id)
    {
        return context.BillDetails.FirstOrDefault(p => p.Id == id);
        // return context.BillDetailss.SingleOrDefault(p => p.Id == id);
    }

    //public List<BillDetails> GetByName(string name)
    //{
    //    return context.BillDetails.Where(p => p.BillDetailsName.Contains(name)).ToList();
    //}

    public bool Update(BillDetails p)
    {
        try
        {// Find(id) chỉ dùng được khi id là khóa chính
            var BillDetails = context.BillDetails.Find(p.Id);
            BillDetails.ProductId = p.ProductId;
            BillDetails.Quantity = p.Quantity;
            BillDetails.Price = p.Price;
            // Có thể sửa thêm thuộc tính
            context.BillDetails.Update(BillDetails);
            context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
