using System;
using System.Collections.Generic;

namespace Prueba_Tecnica_Italika.Models;

public partial class Teacher
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string IdentificationNumber { get; set; } = null!;

    public int SchoolId { get; set; }

    public virtual School School { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
