using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class UserRole
{
    public int Id { get; set; }

    public string? DisplayName { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
