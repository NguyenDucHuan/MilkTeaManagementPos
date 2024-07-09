using System;
using System.Collections.Generic;

namespace MilkTeaManagement.DAL.Entities;

public partial class TbGroupProduct
{
    public long IdGr { get; set; }

    public string? NameGr { get; set; }

    public string? DescriptionGr { get; set; }

    public virtual ICollection<TbProduct> TbProducts { get; set; } = new List<TbProduct>();
}
