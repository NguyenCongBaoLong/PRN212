﻿using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string? DisplayName { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? MoreInfo { get; set; }

    public DateTime? ContractDate { get; set; }

    public virtual ICollection<OutputInfo> OutputInfos { get; set; } = new List<OutputInfo>();
}
