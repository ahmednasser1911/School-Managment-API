using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.Models.ViewModels
{
    public class StudentCourseVM
    {
        public int Fees { get; set; }
        public string Name { get; set; }
        public bool IsPaid { get; set; }
        public List<string> Courses { get; set; }

    }
}
