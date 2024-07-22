using System;
using System.Collections.Generic;

namespace MilkTeaManagement.DAL.Entities;

public partial class TbRole
{
    public int Id { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<LoginRole> LoginRoles { get; set; } = new List<LoginRole>();
}
