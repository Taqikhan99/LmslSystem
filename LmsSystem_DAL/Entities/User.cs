﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmsSystem_DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Phone { get; set; }
        public Nullable <DateTime> JoinedDate { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
       
    }
}
