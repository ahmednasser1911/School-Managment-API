using SchoolManagment.Models;
using SchoolManagment.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.Services.Interfaces
{
    public interface ICourseRepo
    {
        Task<CourseStudentTeacherVM> AddCourse(CourseStudentTeacherDto course);
        Task<CourseStudentTeacherVM> GetCourse(int Id);
        Task<string> DeleteCourse(int Id);
        Task<List<CourseStudentTeacherVM>> GetCourses();
    }
}
