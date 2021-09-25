using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Field { get; set; }
        public DateTime Duration { get; set; }
        public int Fees { get; set; }

        // Navigation props ---> Teacher , Students
        public List<StudentCourse> StudentCourses { get; set; }
        public List<CourseTeachers> CoursesTeachers { get; set; }


    }
}
