using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmsSystem_DAL.Entities
{
    public class Class
    {
        public int ClassId { get; set; }
        public int ClassRoomId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }

        public string ClassDay { get; set; }

        public DateTime ClassTime { get; set; }

    }
}
