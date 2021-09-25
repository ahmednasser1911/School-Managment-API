using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.Models.ViewModels
{
    public class TeacherDto
    {
        public string Name { get; set; }
        public string Field { get; set; }
        public List<int> Courses { get; set; }
    }
}
