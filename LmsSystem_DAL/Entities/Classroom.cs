using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmsSystem_DAL.Entities
{
    public class Classroom
    {
        public int Id { get; set; }
        public string RoomName { get; set; }

        public int Capacity { get; set; }
    }
}
