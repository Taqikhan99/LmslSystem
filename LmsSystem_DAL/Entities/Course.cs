using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmsSystem_DAL.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string ProgramName { get; set; }
        public int ProgramId { get; set; }
    }
}
