using System;
using System.Collections.Generic;

namespace Prueba_Tecnica_Finanzauto.Models;

public partial class Teacher
{
    public int Id { get; set; }

    public string Names { get; set; } = null!;

    public string Surnames { get; set; } = null!;

    public string? Email { get; set; }

    public DateTime CreationDate { get; set; }

    public string CreationUser { get; set; } = null!;

    public DateTime? ModificationDate { get; set; }

    public string? ModificationUser { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
