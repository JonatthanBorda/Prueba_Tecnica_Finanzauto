using System;
using System.Collections.Generic;

namespace Prueba_Tecnica_Finanzauto.Models;

public partial class Grade
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public int StudentId { get; set; }

    public double? Grade1 { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
