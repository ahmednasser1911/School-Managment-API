using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagment.Models.ViewModels
{
    public class StudentDto
    {
        public bool IsPaid { get; set; }
        public int Fees { get; set; }
        public string Name { get; set; }
        public List<int> Courses { get; set; }

    }
}
