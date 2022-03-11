using System;
using System.Collections.Generic;

namespace WorkdayNet.Strategies
{
    public class NegativeNumbersStrategy : ICalendarStrategty
    {
        public DateTime AdditionalDaysForHolidays(DateTime calculationStartTime, DateTime calculationEndTime, List<DateTime> holidays)
        {
            DateTime nextBusinessDay = calculationEndTime;
            foreach (DateTime holiday in holidays)
            {
                if (holiday.Date <= calculationStartTime.Date && holiday.Date >= calculationEndTime.Date && !Utils.CalendarUtils.IsWeekendDay(holiday.DayOfWeek))
                {
                    nextBusinessDay = nextBusinessDay.AddDays(-1);
                }
            }
            return nextBusinessDay;
        }

        public DateTime AdditionalDaysForWeekend(DateTime firstWeekDays, int remainingDays)
        {
            int additionaDays = 0;

            DateTime previousDay = firstWeekDays;
            for (int i = remainingDays; i <= 0; i++)
            {
                if (Utils.CalendarUtils.IsWeekendDay(previousDay.DayOfWeek))
                {
                    additionaDays--;
                }
                previousDay = previousDay.AddDays(-1);
            }

            return firstWeekDays.AddDays(additionaDays);
        }

        public DateTime CalculateWorkingHours(DateTime bussinesDay, TimeSpan startWorkingHours, TimeSpan endWoringHours)
        {
            DateTime nextBussinesDay = bussinesDay;
            TimeSpan currentHours = bussinesDay.TimeOfDay;

            if (currentHours < startWorkingHours)
            {
                TimeSpan nextDaysHours = TimeSpan.FromHours(00) + currentHours;

                bussinesDay = bussinesDay.AddDays(-1);
                nextBussinesDay = bussinesDay.Date + startWorkingHours.Add(nextDaysHours);
            }
            else if (currentHours > endWoringHours) 
            {
                TimeSpan nextDaysHoursToAdd = endWoringHours + currentHours;

                bussinesDay = bussinesDay.AddDays(-1);
                nextBussinesDay = bussinesDay.Date + startWorkingHours.Add(nextDaysHoursToAdd);
            }

            return nextBussinesDay;
        }
    }
}
