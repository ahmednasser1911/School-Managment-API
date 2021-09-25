using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.Models.ViewModels
{
    public class CourseStudentTeacherDto
    {
        public int DurationDays { get; set; }
        public int Fees { get; set; }
        public string Field { get; set; }
        public List<int> Teachers { get; set; }
        public List<int> Students { get; set; }


    }
}
