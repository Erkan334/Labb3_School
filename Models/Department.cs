using System;
using System.Collections.Generic;

namespace Labb3_School.Models;

public partial class Department
{
    public int Id { get; set; }

    public string Department1 { get; set; } = null!;

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
