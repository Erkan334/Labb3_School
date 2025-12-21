using System;
using System.Collections.Generic;

namespace Labb3_School.Models;

public partial class GradesLastMonth
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Course { get; set; } = null!;

    public string Grade { get; set; } = null!;

    public DateOnly GradeSetDate { get; set; }
}
