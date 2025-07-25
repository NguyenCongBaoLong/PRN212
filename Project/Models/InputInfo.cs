using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class InputInfo
{
    public int Id { get; set; }

    public int? IdObject { get; set; }

    public int? IdInput { get; set; }

    public int? Count { get; set; }

    public decimal? InputPrice { get; set; }

    public string? Status { get; set; }

    public virtual Input? IdInputNavigation { get; set; }

    public virtual Objects? IdObjectNavigation { get; set; }
}
