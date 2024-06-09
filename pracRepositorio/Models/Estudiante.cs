using System;
using System.Collections.Generic;

namespace pracRepositorio.Models;

public partial class Estudiante
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int Edad { get; set; }

    public int? IdCurso { get; set; }

    public virtual Curso? IdCursoNavigation { get; set; }
}
