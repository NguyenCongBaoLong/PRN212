using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class Objects
{
    public int Id { get; set; }

    public string? DisplayName { get; set; }

    public int? Quantity { get; set; }

    public int? IdSupplier { get; set; }

    public int? IdUnit { get; set; }

    public string? QrCode { get; set; }

    public string? BarCode { get; set; }

    public virtual Supplier? IdSupplierNavigation { get; set; }

    public virtual Unit? IdUnitNavigation { get; set; }

    public virtual ICollection<InputInfo> InputInfos { get; set; } = new List<InputInfo>();

    public virtual ICollection<OutputInfo> OutputInfos { get; set; } = new List<OutputInfo>();
}
