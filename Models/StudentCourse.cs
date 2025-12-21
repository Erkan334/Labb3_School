using System;
using System.Collections.Generic;

namespace Labb3_School.Models;

public partial class StudentCourse
{
    public int StudentId { get; set; }

    public int CourseId { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Grade? Grade { get; set; }

    public virtual Student Student { get; set; } = null!;
}
