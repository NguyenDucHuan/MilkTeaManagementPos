using System;
using System.Collections.Generic;

namespace MilkTeaManagement.DAL.Entities;

public partial class TbTable
{
    public long Id { get; set; }

    public string? NameTb { get; set; }

    public string? Description { get; set; }

    public long? IdGroup { get; set; }

    public bool? Status { get; set; }

    public virtual TbGroupTb? IdGroupNavigation { get; set; }

    public virtual ICollection<TbBill> TbBills { get; set; } = new List<TbBill>();
}
