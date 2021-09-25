using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Models.ViewModels;
using SchoolManagment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepo studentRepo;

        public StudentController(IStudentRepo studentRepo)
        {
            this.studentRepo = studentRepo;
        }

        [HttpGet("GetStudent")]
        public async Task<IActionResult> GetStudent(int Id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var student = await studentRepo.GetStudent(Id);
            if (student is null) return NotFound(new { Message = $"Student with Id {Id} Not Found!" });

            return Ok(new { Student = student });
        }

        [HttpGet("ListStudents")]
        public async Task<IActionResult> ListTeachers()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var students = await studentRepo.GetStudents();
            if (!students.Any()) return NotFound(new { Message = $"No Students Added!" });

            return Ok(new { Students = students });
        }

        [HttpPost("AddStudent")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AddStudent(StudentDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var student = await studentRepo.AddStudent(model);

            return Ok(new { Student = student });
        }

        [HttpDelete("DeleteStudent")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var message = await studentRepo.DeleteStudent(id);
            if (message is null) return NotFound(new { Message = "Student Not Found!" });

            return Ok(new { Message = message });
        }
    }
}
