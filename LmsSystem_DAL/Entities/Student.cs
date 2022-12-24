using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmsSystem_DAL.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Nullable<DateTime> Birthdate { get; set; }
        public int Age { get; set; }
        public int Semester { get; set; }
        public int DepartId { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public int RoleID { get; set; }

        
        public string Departname { get; set; }
        public Nullable<DateTime> Joineddate { get; set; }
    }
}
