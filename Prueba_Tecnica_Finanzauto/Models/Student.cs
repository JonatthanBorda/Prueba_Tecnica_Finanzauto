using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_Tecnica_Finanzauto.Models;

public partial class Student
{
    public int Id { get; set; }

    public required string Names { get; set; } = null!;

    public required string Surnames { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string? Email { get; set; }

    public DateTime CreationDate { get; set; }
    public string CreationUser { get; set; } = null!;
    public DateTime? ModificationDate { get; set; }
    public string? ModificationUser { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}

public class CreateStudentDto
{
    public required string Names { get; set; }
    public required string Surnames { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Email { get; set; }
}

public class UpdateStudentDto
{
    public string? Names { get; set; }
    public string? Surnames { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Email { get; set; }
}
