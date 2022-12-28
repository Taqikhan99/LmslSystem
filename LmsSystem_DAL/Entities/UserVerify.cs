﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmsSystem_DAL.Entities
{
    public class UserVerify
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="UserName is Required!")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is Required!")]
        public string Password { get; set; }
    }
}
