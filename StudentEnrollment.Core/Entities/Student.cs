using System;
using System.Collections.Generic;

namespace StudentEnrollment.Core.Entities;

public partial class Student
{
    public int StudentId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
