using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LmsSystem_DAL.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int Salary { get; set; }

        public string Designation { get; set; }
        public int DepartId { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public int RoleID { get; set; }

        public string DepartName { get; set; }
        public Nullable<DateTime> Joineddate { get; set; }

        public HttpPostedFileBase UserPic { get; set; }
        public string UserPicPath { get; set; }
    }
}
