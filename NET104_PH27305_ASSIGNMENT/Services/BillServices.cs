using NET104_PH27305_ASSIGNMENT.IServices;
using NET104_PH27305_ASSIGNMENT.Models;

namespace NET104_PH27305_ASSIGNMENT.Services;

public class BillServices : IBillServices
{
    ShopDbContext context;

    public BillServices()
    {
        context = new ShopDbContext();
    }

    public bool Create(Bill p)
    {
        try
        {
            context.Bills.Add(p);
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
            var Bill = context.Bills.Find(id);
            context.Bills.Remove(Bill);
            context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public List<Bill> GetAll()
    {
        return context.Bills.ToList();
    }

    public Bill GetById(Guid id)
    {
        return context.Bills.FirstOrDefault(p => p.Id == id);
        // return context.Bills.SingleOrDefault(p => p.Id == id);
    }

    //public List<Bill> GetByName(string name)
    //{
    //    return context.Bill.Where(p => p.BillName.Contains(name)).ToList();
    //}

    public bool Update(Bill p)
    {
        try
        {// Find(id) chỉ dùng được khi id là khóa chính
            var Bill = context.Bills.Find(p.Id);
            Bill.Status = p.Status;
            // Có thể sửa thêm thuộc tính
            context.Bills.Update(Bill);
            context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
