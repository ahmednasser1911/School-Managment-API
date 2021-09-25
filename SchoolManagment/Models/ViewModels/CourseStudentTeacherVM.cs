using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.Models.ViewModels
{
    public class CourseStudentTeacherVM
    {
        public DateTime Duration { get; set; }
        public int Fees { get; set; }
        public string Field { get; set; }

        public List<string> Students { get; set; }
        public List<string> Teachers { get; set; }

    }
}
