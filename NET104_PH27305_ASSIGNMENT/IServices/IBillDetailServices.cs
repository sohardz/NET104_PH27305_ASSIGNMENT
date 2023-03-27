using SellerProduct.Models;

namespace SellerProduct.IServices;

public interface IBillDetailServices
{
    public bool Create(BillDetails p);
    public bool Update(BillDetails p);
    public bool Delete(Guid id);
    public List<BillDetails> GetAll();
    public BillDetails GetById(Guid id);
    //public List<BillDetails> GetByName(string name);
}
