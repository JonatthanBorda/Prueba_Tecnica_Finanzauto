using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Prueba_Tecnica_Finanzauto.Models;

namespace Prueba_Tecnica_Finanzauto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly DbApiFinanzautoContext _context;

        public StudentsController(DbApiFinanzautoContext context)
        {
            _context = context;
        }

        // GET: api/Students
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        //{
        //    return await _context.Students.ToListAsync();
        //}


        // GET: api/Students
        //Por defecto el método devolverá resultados de la página 1 con limite de 10 registros:
        [HttpGet]
        public async Task<ActionResult> GetStudents([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            //Se realiza el conteo de estudiantes totales en la BD:
            int totalCount = await _context.Students.CountAsync();
            //Se calcula el número de páginas resultante con relación a la cantidad de registros máximos mostrados en 1 página:
            int totalPages = (int)Math.Ceiling(totalCount / (double)limit);

            //Sí el total de páginas calculado esta en el rango de páginas a mostrar, se asigna el valor: 
            if (page > totalPages) page = totalPages;
            if (page < 1) page = 1;

            //Se segmenta el número de resultados al limite:
            var students = await _context.Students
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();

            var result = new
            {
                data = students.Select(s => new {
                    s.Id,
                    s.Names,
                    s.Surnames,
                    s.BirthDate,
                    s.Email,
                    s.CreationDate,
                    s.CreationUser,
                    s.ModificationDate,
                    s.ModificationUser,
                    s.Grades
                }),
                total = totalCount,
                page,
                limit,
                totalPages
            };

            return Ok(result);
        }


        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, UpdateStudentDto studentDto)
        {
            //Se busca el estudiante a modificar por id:
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                //Si no existe, retorna 404 - No encontrado
                return NotFound();
            }

            //Se actualizan solo los campos proporcionados en el JSON request:
            if (!string.IsNullOrEmpty(studentDto.Names))
            {
                student.Names = studentDto.Names;
            }
            if (studentDto.BirthDate != null)
            {
                student.BirthDate = studentDto.BirthDate.Value;
            }
            if (!string.IsNullOrEmpty(studentDto.Surnames))
            {
                student.Surnames = studentDto.Surnames;
            }
            if (!string.IsNullOrEmpty(studentDto.Email))
            {
                student.Email = studentDto.Email;
            }

            //Campos de auditoria:
            student.ModificationDate = DateTime.Now;
            student.ModificationUser = "System_API";

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Students.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(CreateStudentDto studentDto)
        {
            //Se crea el estudiante nuevo con los datos del objeto studentDTO:
            var student = new Student
            {
                Names = studentDto.Names,
                Surnames = studentDto.Surnames,
                BirthDate = studentDto.BirthDate,
                Email = studentDto.Email,
                //Campos de auditoria:
                CreationDate = DateTime.Now,
                CreationUser = "System_API"
            };

            //Se agrega a la BD y se guardan cambios:
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            //Retorna estado 201 - Estudiante creado con los datos
            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }


        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
