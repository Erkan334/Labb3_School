using System;
using System.Collections.Generic;

namespace Labb3_School.Models;

public partial class Staff
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Role { get; set; } = null!;

    public int? Age { get; set; }

    public string? Email { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public int? DepartmentId { get; set; }

    public decimal? Salary { get; set; }

    public DateOnly? HireDate { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
