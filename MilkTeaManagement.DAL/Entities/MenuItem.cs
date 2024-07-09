using System;
using System.Collections.Generic;

namespace MilkTeaManagement.DAL.Entities;

public partial class MenuItem
{
    public int Id { get; set; }

    public string? NameMenu { get; set; }

    public string? NameShow { get; set; }

    public virtual ICollection<LoginRole> LoginRoles { get; set; } = new List<LoginRole>();
}
