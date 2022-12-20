using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmsSystem_DAL.Entities
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public Nullable<DateTime> StartDate { get; set; }
        public string StartYear { get; set; }
        public int StudentsEnrolled { get; set; }

    }
}
