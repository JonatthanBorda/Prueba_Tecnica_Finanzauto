using System;
using System.Collections.Generic;

namespace Prueba_Tecnica_Finanzauto.Models;

public partial class Course
{
    public int Id { get; set; }

    public string? CourseName { get; set; }

    public int NumberRegistered { get; set; }

    public int TeacherId { get; set; }

    public DateTime CreationDate { get; set; }

    public string CreationUser { get; set; } = null!;

    public DateTime? ModificationDate { get; set; }

    public string? ModificationUser { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Teacher Teacher { get; set; } = null!;
}
