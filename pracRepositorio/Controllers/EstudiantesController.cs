﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCEF.Repositorio;
using pracRepositorio.Models;

namespace pracRepositorio.Controllers
{
    public class EstudiantesController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public EstudiantesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Estudiantes
        public async Task<IActionResult> Index()
        {
            var estudiantes = _unitOfWork.Estudiantes.GetAllWithIncludes(e => e.IdCursoNavigation);
            return View(await Task.FromResult(estudiantes));
        }

        // GET: Estudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = _unitOfWork.Estudiantes.GetByIdWithIncludes(id.Value, e => e.IdCursoNavigation);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(await Task.FromResult(estudiante));
        }

        // GET: Estudiantes/Create
        public IActionResult Create()
        {
            ViewData["IdCurso"] = new SelectList(_unitOfWork.Cursos.GetAll(), "Id", "Id");
            return View();
        }

        // POST: Estudiantes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Edad,IdCurso")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Estudiantes.Insert(estudiante);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCurso"] = new SelectList(_unitOfWork.Cursos.GetAll(), "Id", "Id", estudiante.IdCurso);
            return View(estudiante);
        }

        // GET: Estudiantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = _unitOfWork.Estudiantes.GetById(id.Value);
            if (estudiante == null)
            {
                return NotFound();
            }
            ViewData["IdCurso"] = new SelectList(_unitOfWork.Cursos.GetAll(), "Id", "Id", estudiante.IdCurso);
            return View(estudiante);
        }

        // POST: Estudiantes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Edad,IdCurso")] Estudiante estudiante)
        {
            if (id != estudiante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Estudiantes.Update(estudiante);
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteExists(estudiante.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCurso"] = new SelectList(_unitOfWork.Cursos.GetAll(), "Id", "Id", estudiante.IdCurso);
            return View(estudiante);
        }

        // GET: Estudiantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = _unitOfWork.Estudiantes.GetByIdWithIncludes(id.Value, e => e.IdCursoNavigation);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(await Task.FromResult(estudiante));
        }

        // POST: Estudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudiante = _unitOfWork.Estudiantes.GetById(id);
            if (estudiante != null)
            {
                _unitOfWork.Estudiantes.Delete(id);
                _unitOfWork.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteExists(int id)
        {
            return _unitOfWork.Estudiantes.GetAll().Any(e => e.Id == id);
        }


    }
}