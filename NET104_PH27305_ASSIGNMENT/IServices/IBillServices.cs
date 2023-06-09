﻿using NET104_PH27305_ASSIGNMENT.Models;

namespace NET104_PH27305_ASSIGNMENT.IServices;

public interface IBillServices
{
    public bool Create(Bill p);
    public bool Update(Bill p);
    public bool Delete(Guid id);
    public List<Bill> GetAll();
    public Bill GetById(Guid id);
    //public List<Bill> GetByName(string name);
}
