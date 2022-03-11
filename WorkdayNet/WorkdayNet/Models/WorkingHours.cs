using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkdayNet.Models
{
    public class WorkingHours
    {
        public WorkingHours(int _startHours, int _startMinutes, int _stopHours, int _stopMinutes)
        {
            StartWoringTime = new TimeSpan(_startHours, _startMinutes, 0);
            EndWoringTime = new TimeSpan(_stopHours, _stopMinutes, 0);
        }

        public TimeSpan StartWoringTime { get; set; }
        public TimeSpan EndWoringTime { get; set; }
    }
}
