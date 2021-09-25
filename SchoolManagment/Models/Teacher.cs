using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.Models
{
    public class Teacher : Person
    {
        // Navigation props ---> course
        public string Field { get; set; }
        public List<CourseTeachers> CoursesTeachers { get; set; }
    }
}
