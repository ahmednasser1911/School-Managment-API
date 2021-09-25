using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Models;
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
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepo teacherRepo;

        public TeacherController(ITeacherRepo teacherRepo)
        {
            this.teacherRepo = teacherRepo;
        }

        [HttpGet("GetTeacher")]
        public async Task<IActionResult> GetTeacher(int Id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var teacher = await teacherRepo.GetTeacher(Id);
            if (teacher is null) return NotFound(new { Message = $"Teacher with Id {Id} Not Found!" });

            return Ok(new { Teacher = teacher });
        }

        [HttpGet("ListTeachers")]
        public async Task<IActionResult> ListTeachers()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var teachers = await teacherRepo.GetTeachers();
            if (! teachers.Any()) return NotFound(new { Message = $"No Teachers Added!" });

            return Ok(new { Teachers = teachers });
        }

        [HttpPost("AddTeacher")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddTeacher(TeacherDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var teacher = await teacherRepo.AddTeacher(model);

            return Ok(new { Teacher = teacher });
        }

        [HttpDelete("DeleteTeacher")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteTeacher(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var message = await teacherRepo.DeleteTeacher(id);
            if (message is null) return NotFound(new { Message = "Teacher Not Found!" });

            return Ok(new { Message = message });
        }
    }
}
