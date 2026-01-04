using System;
using System.Collections.Generic;

namespace Labb3_School.Models;

public partial class Grade
{
    public string Grade1 { get; set; } = null!;

    public int StudentId { get; set; }
    public virtual Student Student { get; set; }

    public int CourseId { get; set; }
    public virtual Course Course { get; set; }

    public DateOnly GradeSetDate { get; set; }

    public int? StaffId { get; set; }

    public virtual StudentCourse StudentCourse { get; set; } = null!;
}
