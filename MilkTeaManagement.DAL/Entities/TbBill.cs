using System;
using System.Collections.Generic;

namespace MilkTeaManagement.DAL.Entities;

public partial class TbBill
{
    public long Id { get; set; }

    public long? IdTable { get; set; }

    public DateTime? BillDate { get; set; }

    public double? TotalMoney { get; set; }

    public bool? Status { get; set; }

    public string? Description { get; set; }

    public long? IdUser { get; set; }

    public long? IdCustomer { get; set; }

    public virtual TbCustomer? IdCustomerNavigation { get; set; }

    public virtual TbTable? IdTableNavigation { get; set; }

    public virtual Login? IdUserNavigation { get; set; }

    public virtual ICollection<TbBillDetailt> TbBillDetailts { get; set; } = new List<TbBillDetailt>();
}
