using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class OutputInfo
{
    public int Id { get; set; }

    public int? IdObject { get; set; }

    public int? IdOutput { get; set; }

    public int? IdCustomer { get; set; }

    public int? Count { get; set; }

    public string? Status { get; set; }

    public decimal? OutputPrice { get; set; }

    public virtual Customer? IdCustomerNavigation { get; set; }

    public virtual Objects? IdObjectNavigation { get; set; }

    public virtual Output? IdOutputNavigation { get; set; }
}
