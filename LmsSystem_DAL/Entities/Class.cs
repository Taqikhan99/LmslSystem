﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmsSystem_DAL.Entities
{
    public class Class
    {
        public int Id { get; set; }
        
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int CourseId { get; set; }

        public string ClassDay { get; set; }

        public int SlotId { get; set; }
        public string Room { get; set; }
        public string CourseName { get; set; }
        public string ClassTime { get; set; }
        public int ClassRoomId { get; set; }

        public string DepartName { get; set; }
        public string ProgramName { get; set; }
        public int ProgramId { get; set; }
        

    }
}
