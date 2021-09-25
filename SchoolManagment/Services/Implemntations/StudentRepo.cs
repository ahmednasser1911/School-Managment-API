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
    public class StudentRepo : IStudentRepo
    {
        private readonly AppDbContext appDbContext;

        public StudentRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<StudentCourseVM> AddStudent(StudentDto model)
        {
            var newStudent = new Student()
            {
                Name = model.Name,
                IsPaid = model.IsPaid,
                Fees = model.Fees
            };

            await appDbContext.Students.AddAsync(newStudent);
            await appDbContext.SaveChangesAsync();


            var student = appDbContext.Students.OrderBy(s => s.Id).Last();

            foreach (var Id in model.Courses)
            {
                var course = await appDbContext.Courses.FindAsync(Id);
                if (course is not null)
                {
                    await appDbContext.StudentsCourses.AddAsync(
                        new StudentCourse { CourseID = Id, StudentID = student.Id }
                        );
                    await appDbContext.SaveChangesAsync();
                }
            }

            return await GetStudent(student.Id);
        }

        public async Task<string> DeleteStudent(int Id)
        {
            var student = await appDbContext.Students.FindAsync(Id);
            if (student is not null)
            {
                appDbContext.Students.Remove(student);
                await appDbContext.SaveChangesAsync();
                return $"Student {student.Name} Deleted!";
            }
            return null;
        }

        public async Task<StudentCourseVM> GetStudent(int Id)
        {
            var student = await appDbContext.Students.FindAsync(Id);
            if (student is null) return null;

            var coursesIds = appDbContext.StudentsCourses.Where(st => st.StudentID== student.Id)
                .Select(st => st.CourseID).ToList();
            List<string> coursesFields = new();

            foreach (var item in coursesIds)
            {
                var course = await appDbContext.Courses.FindAsync(item);
                if (course is not null)
                    coursesFields.Add(course.Field);
            }

            if (!coursesFields.Any())
                return new StudentCourseVM
                {
                    Name = student.Name,
                    Fees = student.Fees,
                    IsPaid = student.IsPaid,
                    Courses = new() { $"No Courses For Mr/Mrs. {student.Name}" }
                };

            return new StudentCourseVM
            {
                Name = student.Name,
                Fees = student.Fees,
                IsPaid = student.IsPaid,
                Courses = coursesFields
            };
        }

        public async Task<List<StudentCourseVM>> GetStudents()
        {
            var students = appDbContext.Students.ToList();
            if (!students.Any()) return null;

            var studentsCourses = new List<StudentCourseVM>();
            foreach (var student in students)
            {
                studentsCourses.Add(await GetStudent(student.Id));
            }
            return studentsCourses;
        }
    }
}
