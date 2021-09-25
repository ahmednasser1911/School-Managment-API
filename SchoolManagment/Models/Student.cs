using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.Models
{
    public class Student : Person
    {
        public int Fees { get; set; }
        public bool IsPaid { get; set; }

        // Navigation props ---> courses enrolled
        public List<StudentCourse> StudentCourses { get; set; }
    }
}
