using System;
using System.Collections.Generic;
using WorkdayNet.Models;
using WorkdayNet.Strategies;

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
            SetCalculationStrategy(incrementInWorkdays);

            AppendRecurringHolidaysToList(startDate);

            return Utils.CalendarUtils.GetBusinessDays(startDate, incrementInWorkdays, holidays, StartWoringTime, EndWoringTime);
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

        private void AppendRecurringHolidaysToList(DateTime startDate) 
        {
            foreach (RecurringHoliday reccuringHoliday in recurringHolidays)
            {
                holidays.Add(new DateTime(startDate.Year, reccuringHoliday.Month, reccuringHoliday.Day));
            }
        }

        private void SetCalculationStrategy(decimal incrementInWorkdays) 
        {
            if (incrementInWorkdays < 0)
            {
                Strategies.Context.Strategy = new NegativeNumbersStrategy();
            }
            else 
            {
                Strategies.Context.Strategy = new PositiveNumbersStrategy();
            }
        }
    }
}
