using SchoolManagment.Models;
using SchoolManagment.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.Services.Interfaces
{
    public interface IStudentRepo
    {
        Task<StudentCourseVM> AddStudent(StudentDto student);
        Task<StudentCourseVM> GetStudent(int Id);
        Task<string> DeleteStudent(int Id);
        Task<List<StudentCourseVM>> GetStudents();
    }
}
