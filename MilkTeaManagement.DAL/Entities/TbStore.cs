using System;
using System.Collections.Generic;

namespace MilkTeaManagement.DAL.Entities;

public partial class TbStore
{
    public long Id { get; set; }

    public string? NameStore { get; set; }

    public string? AddressStore { get; set; }

    public string? PhoneStore { get; set; }

    public string? TaxCode { get; set; }
}
