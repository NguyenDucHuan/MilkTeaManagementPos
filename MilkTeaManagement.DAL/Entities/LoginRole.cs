using System;
using System.Collections.Generic;

namespace MilkTeaManagement.DAL.Entities;

public partial class LoginRole
{
    public long Id { get; set; }

    public long IdLogin { get; set; }

    public int IdRole { get; set; }

    public virtual Login IdLoginNavigation { get; set; } = null!;

    public virtual TbRole IdRoleNavigation { get; set; } = null!;
}
