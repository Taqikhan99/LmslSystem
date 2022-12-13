using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmsSystem_DAL.Entities
{
    public class UsersByDepart
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string DepartName { get; set; }


    }
}
