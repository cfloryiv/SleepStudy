using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SleepStudy.Data.Models
{
    public class SleepEntry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime WakeTime { get; set; }
        public string Note { get; set; }

    }
}
