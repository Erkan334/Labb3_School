using System;
using System.Collections.Generic;

namespace Labb3_School.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Course1 { get; set; } = null!;

    public int Points { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
}
