using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkdayNet.Models;
using WorkdayNet.Utils;

namespace WorkdayNet
{
    internal class WorkdayCalendar : IWorkdayCalendar
    {
        private static readonly List<DateTime> holidays = new List<DateTime>();
        private static readonly List<RecurringHoliday> recurringHolidays = new List<RecurringHoliday>();
        private static TimeSpan StartWoringTime;
        private static TimeSpan EndWoringTime;

        public DateTime GetWorkdayIncrement(DateTime startDate, decimal incrementInWorkdays)
        {
            foreach (RecurringHoliday reccuringHoliday in recurringHolidays) 
            { 
                holidays.Add(new DateTime(startDate.Year, reccuringHoliday.Month, reccuringHoliday.Day));
            }

            return Utils.CalendarUtils.GetBusinesDays(startDate, incrementInWorkdays, holidays, StartWoringTime, EndWoringTime);
        }

        public void SetHoliday(DateTime date)
        {
            holidays.Add(date);
        }

        public void SetRecurringHoliday(int month, int day)
        {
            recurringHolidays.Add(new RecurringHoliday(month, day));
        }

        public void SetWorkdayStartAndStop(int startHours, int startMinutes, int stopHours, int stopMinutes)
        {
            StartWoringTime = new TimeSpan(startHours, startMinutes, 0);
            EndWoringTime = new TimeSpan(stopHours, stopMinutes, 0);
        }
    }
}
