﻿using SellerProduct.Models;

namespace SellerProduct.IServices;

public interface IProductServices
{
    public bool Create(Product p);
    public bool Update(Product p);
    public bool Delete(Guid id);
    public List<Product> GetAll();
    public Product GetById(Guid id);
    public List<Product> GetByName(string name);

}
