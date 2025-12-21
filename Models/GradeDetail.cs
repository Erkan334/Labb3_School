using System;
using System.Collections.Generic;

namespace Labb3_School.Models;

public partial class GradeDetail
{
    public string Course { get; set; } = null!;

    public int Id { get; set; }

    public string AverageGrade { get; set; } = null!;

    public string HighestGrade { get; set; } = null!;

    public string LowestGrade { get; set; } = null!;
}
