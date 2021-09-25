using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.Models.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(s => s.Student)
                .WithMany(sc => sc.StudentCourses)
                .HasForeignKey(si => si.StudentID);

            modelBuilder.Entity<StudentCourse>()
            .HasOne(c => c.Course)
            .WithMany(sc => sc.StudentCourses)
            .HasForeignKey(ci => ci.CourseID);

            modelBuilder.Entity<CourseTeachers>()
                .HasOne(c => c.Course)
                .WithMany(tc => tc.CoursesTeachers)
                .HasForeignKey(ci => ci.CourseId);

            modelBuilder.Entity<CourseTeachers>()
                .HasOne(t => t.Teacher)
                .WithMany(tc => tc.CoursesTeachers)
                .HasForeignKey(ti => ti.TeacherId);

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentsCourses { get; set; }
        public DbSet<CourseTeachers> CoursesTeachers { get; set; }


    }
}
