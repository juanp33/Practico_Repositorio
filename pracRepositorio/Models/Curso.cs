using System;
using System.Collections.Generic;

namespace pracRepositorio.Models;

public partial class Curso
{
    public int Id { get; set; }

    public string NombreCurso { get; set; } = null!;

    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
}
