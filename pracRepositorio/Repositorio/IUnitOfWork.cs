
using pracRepositorio.Models;
using System;
namespace MVCEF.Repositorio
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Curso> Cursos { get; }
        IGenericRepository<Estudiante> Estudiantes { get; }
        void Save();
    }
}
