using System;
using System.Collections.Generic;

namespace MilkTeaManagement.DAL.Entities;

public partial class TbBillDetailt
{
    public long Id { get; set; }

    public double? UnitPrice { get; set; }

    public int? Quantity { get; set; }

    public long? IdBill { get; set; }

    public long? IdProduct { get; set; }

    public double? IntoMoney { get; set; }

    public string? Description { get; set; }

    public virtual TbBill? IdBillNavigation { get; set; }

    public virtual TbProduct? IdProductNavigation { get; set; }
}
