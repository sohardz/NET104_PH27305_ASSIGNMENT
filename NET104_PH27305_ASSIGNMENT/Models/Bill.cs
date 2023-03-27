﻿namespace SellerProduct.Models;

public class Bill
{
    public Guid Id { get; set; }
    public DateTime DateofCreation { get; set; }
    public Guid UserId { get; set; }
    public int Status { get; set; }
    public virtual ICollection<BillDetails> BillDetails { get; set; }
    public virtual User User { get; set; }
}
