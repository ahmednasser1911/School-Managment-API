using SchoolManagment.Models;
using SchoolManagment.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.Services.Interfaces
{
    public interface ITeacherRepo
    {
        Task<TeacherCoursesVM> AddTeacher(TeacherDto teacher);
        Task<TeacherCoursesVM> GetTeacher(int Id);
        Task<string> DeleteTeacher(int Id);
        Task<List<TeacherCoursesVM>> GetTeachers();

    }
}
