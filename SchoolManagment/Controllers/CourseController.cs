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
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepo courseRepo;

        public CourseController(ICourseRepo courseRepo)
        {
            this.courseRepo = courseRepo;
        }

        [HttpGet("GetCourse")]
        public async Task<IActionResult> GetCourse(int Id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var course = await courseRepo.GetCourse(Id);
            if (course is null) return NotFound(new { Message = $"Course with Id {Id} Not Found!" });

            return Ok(new { course = course });
        }

        [HttpGet("ListCourses")]
        public async Task<IActionResult> ListCourses()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var courses = await courseRepo.GetCourses();
            if (!courses.Any()) return NotFound(new { Message = $"No Courses Added!" });

            return Ok(new { Courses = courses });
        }

        [HttpPost("AddCourse")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AddCourse(CourseStudentTeacherDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var course = await courseRepo.AddCourse(model);

            return Ok(new { Course = course });
        }

        [HttpDelete("DeleteCourse")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var message = await courseRepo.DeleteCourse(id);
            if (message is null) return NotFound(new { Message = "Student Not Found!" });

            return Ok(new { Message = message });
        }
    }
}
