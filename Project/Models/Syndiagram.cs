using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class Syndiagram
{
    public int DiagramId { get; set; }

    public string? Name { get; set; }

    public int? Version { get; set; }

    public byte[]? Definition { get; set; }
}
