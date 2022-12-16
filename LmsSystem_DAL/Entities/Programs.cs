using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmsSystem_DAL.Entities
{
    public class Programs
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int DepartId { get; set; }
    }
}
