using System;
using System.Collections.Generic;

namespace WorkdayNet.Strategies
{
    public interface ICalendarStrategty
    {
       DateTime AdditionalDaysForWeekend(DateTime datetime, int days);

       DateTime AdditionalDaysForHolidays(DateTime calculationStartTime, DateTime calculationEndTime, List<DateTime> holidays);

       DateTime CalculateWorkingHours(DateTime bussinesDay, TimeSpan startWorkingHours, TimeSpan endWoringHours);
    }
}
