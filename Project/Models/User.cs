﻿using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class User
{
    public int Id { get; set; }

    public string? DisplayName { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public int? IdRole { get; set; }

    public virtual UserRole? IdRoleNavigation { get; set; }
}
