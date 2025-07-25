using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class Unit
{
    public int Id { get; set; }

    public string? DisplayName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Objects> Objects { get; set; } = new List<Objects>();
}
