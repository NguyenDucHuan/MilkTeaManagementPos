using System;
using System.Collections.Generic;

namespace MilkTeaManagement.DAL.Entities;

public partial class TbCustomer
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Description { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<TbBill> TbBills { get; set; } = new List<TbBill>();
}
