using SchoolManagment.Models;
using SchoolManagment.Models.Data;
using SchoolManagment.Models.ViewModels;
using SchoolManagment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.Services.Implemntations
{
    public class TeacherRepo : ITeacherRepo
    {
        private readonly AppDbContext appDbContext;

        public TeacherRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<TeacherCoursesVM> AddTeacher(TeacherDto model)
        {
            var newTeacher = new Teacher()
            {
                Name = model.Name,
                Field = model.Field,
            };

            await appDbContext.Teachers.AddAsync(newTeacher);
            await appDbContext.SaveChangesAsync();
            

            var teacher = appDbContext.Teachers.OrderBy(t => t.Id).Last();

            foreach (var Id in model.Courses)
            {
                var course = await appDbContext.Courses.FindAsync(Id);
                if (course is not null) 
                {
                    await appDbContext.CoursesTeachers.AddAsync(
                        new CourseTeachers { CourseId = Id , TeacherId = teacher.Id }
                        );
                     await appDbContext.SaveChangesAsync();
                }
            }

            return await GetTeacher(teacher.Id);
        }

        public async Task<string> DeleteTeacher(int Id)
        {
            var teacher = await appDbContext.Teachers.FindAsync(Id);
            if(teacher is not null)
            {
                appDbContext.Teachers.Remove(teacher);
                await appDbContext.SaveChangesAsync();
                return $"Teacher {teacher.Name} Deleted!";
            }
            return null;
        }

        public async Task<TeacherCoursesVM> GetTeacher(int Id)
        {
            var teacher = await appDbContext.Teachers.FindAsync(Id);
            if (teacher is null) return null;

            var coursesIds = appDbContext.CoursesTeachers.Where(ct => ct.TeacherId == teacher.Id).Select(ct => ct.CourseId).ToList();
            List<string> coursesFields = new();

            foreach (var item in coursesIds)
            {
                var course = await appDbContext.Courses.FindAsync(item);
                if(course is not null)
                    coursesFields.Add(course.Field);
            }

            if (!coursesFields.Any())
                return new TeacherCoursesVM
                    {
                        Name = teacher.Name,
                        Field = teacher.Field,
                        Courses = new() {$"No Courses For Mr/Mrs. {teacher.Name}"}
                    };

            return new TeacherCoursesVM
                {
                    Name = teacher.Name,
                    Field = teacher.Field,
                    Courses = coursesFields
                };
        }

        public async Task<List<TeacherCoursesVM>> GetTeachers()
        {
            var teachers = appDbContext.Teachers.ToList();
            if (!teachers.Any()) return null;

            var teacherCourses = new List<TeacherCoursesVM>();
            foreach (var teacher in teachers)
            {
                teacherCourses.Add(await GetTeacher(teacher.Id));
            }
            return teacherCourses;
        }
    }
}
