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
    public class CourseRepo : ICourseRepo
    {
        private readonly AppDbContext appDbContext;

        public CourseRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<CourseStudentTeacherVM> AddCourse(CourseStudentTeacherDto model)
        {
            var newCourse = new Course()
            {
                Field = model.Field,
                Duration = DateTime.Now.AddDays(model.DurationDays),
                Fees = model.Fees,
            };

            await appDbContext.Courses.AddAsync(newCourse);
            await appDbContext.SaveChangesAsync();


            var course = appDbContext.Courses.OrderBy(c => c.Id).Last();

            foreach (var Id in model.Students)
            {
                var student = await appDbContext.Students.FindAsync(Id);
                if (student is not null)
                {
                    await appDbContext.StudentsCourses.AddAsync(
                        new StudentCourse { CourseID = course.Id, StudentID = student.Id }
                        );
                    await appDbContext.SaveChangesAsync();
                }
            }

            foreach (var Id in model.Teachers)
            {
                var teacher = await appDbContext.Teachers.FindAsync(Id);
                if (teacher is not null)
                {
                    await appDbContext.CoursesTeachers.AddAsync(
                        new CourseTeachers { CourseId = course.Id , TeacherId = Id }
                        );
                    await appDbContext.SaveChangesAsync();
                }
            }
            return await GetCourse(course.Id);
        }

        public async Task<string> DeleteCourse(int Id)
        {
            var course = await appDbContext.Courses.FindAsync(Id);
            if (course is not null)
            {
                appDbContext.Courses.Remove(course);
                await appDbContext.SaveChangesAsync();
                return $"Course {course.Field} Deleted!";
            }
            return null;
        }

        public async Task<CourseStudentTeacherVM> GetCourse(int Id)
        {
            var course = await appDbContext.Courses.FindAsync(Id);
            if (course is null) return null;

            var teacherIds = appDbContext.CoursesTeachers.Where(tc => tc.CourseId == course.Id)
                .Select(st => st.TeacherId).ToList();
            var studentIds = appDbContext.StudentsCourses.Where(cs => cs.CourseID == course.Id)
                .Select(st => st.StudentID).ToList();

            List<string> teachers = new();
            List<string> students = new();

            foreach (var item in teacherIds)
            {
                var teacher = await appDbContext.Teachers.FindAsync(item);
                if (teacher is not null)
                    teachers.Add(teacher.Name);
            }

            foreach (var item in studentIds)
            {
                var student = await appDbContext.Students.FindAsync(item);
                if (student is not null)
                    students.Add(student.Name);
            }
               
            if (teachers.Any() && students.Any())
                return new CourseStudentTeacherVM
                {
                    Fees = course.Fees,
                    Field = course.Field,
                    Duration = course.Duration,
                    Students = students,
                    Teachers = teachers
                };

            else if (!teachers.Any() && students.Any())
                return new CourseStudentTeacherVM
                {
                    Fees = course.Fees,
                    Field = course.Field,
                    Duration = course.Duration,
                    Students = students,
                    Teachers = new() { "No Teachers For This Course" }
                };

            else if (teachers.Any() && !students.Any())
                return new CourseStudentTeacherVM
                {
                    Fees = course.Fees,
                    Field = course.Field,
                    Duration = course.Duration,
                    Students = new() { "No Students For This Course" },
                    Teachers = teachers
                };
            else 
                return new CourseStudentTeacherVM
                {
                    Fees = course.Fees,
                    Field = course.Field,
                    Duration = course.Duration,
                    Students = new() { "No Students For This Course" },
                    Teachers = new() { "No Teachers For This Course" }
                };
        }

        public async Task<List<CourseStudentTeacherVM>> GetCourses()
        {
            var courses = appDbContext.Courses.ToList();
            if (!courses.Any()) return null;

            var courseStudentTeacher = new List<CourseStudentTeacherVM>();
            foreach (var course in courses)
            {
                courseStudentTeacher.Add(await GetCourse(course.Id));
            }
            return courseStudentTeacher;
        }
    }
}
