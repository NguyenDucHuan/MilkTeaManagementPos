using System;
using System.Collections.Generic;

namespace MilkTeaManagement.DAL.Entities;

public partial class Login
{
    public long Id { get; set; }

    public long? IdEmployee { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public bool? IsUse { get; set; }

    public virtual Employee? IdEmployeeNavigation { get; set; }

    public virtual ICollection<LoginRole> LoginRoles { get; set; } = new List<LoginRole>();

    public virtual ICollection<TbBill> TbBills { get; set; } = new List<TbBill>();
}
