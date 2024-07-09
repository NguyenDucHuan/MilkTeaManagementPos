using System;
using System.Collections.Generic;

namespace MilkTeaManagement.DAL.Entities;

public partial class TbGroupTb
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<TbTable> TbTables { get; set; } = new List<TbTable>();
}
