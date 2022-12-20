using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmsSystem_DAL.Entities
{
    public class UserDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
        public Nullable<DateTime> JoinedDate { get; set; }
        public string DepartName { get; set; }

        public string ProgramName { get; set; }
    }
}
