using System;
using System.Collections.Generic;

namespace Labb3_School.Models;

public partial class Class
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Program { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
