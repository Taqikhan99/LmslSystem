using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmsSystem_DAL.Entities
{
    public class TimeSlot
    {
        public int Id { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
