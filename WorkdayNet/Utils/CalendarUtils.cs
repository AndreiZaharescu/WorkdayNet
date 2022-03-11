using System;
using System.Collections.Generic;

namespace WorkdayNet.Utils
{
    public static class CalendarUtils
    {

        public static DateTime GetBusinessDays(DateTime calculationDateTime, decimal numberOfDays, List<DateTime> holidays, TimeSpan startWorkingHours, TimeSpan endWoringHours)
        {
            var nextBusinessDay = GetNextBusinessDay(calculationDateTime, numberOfDays, holidays);
            return Strategies.Context.Strategy.CalculateWorkingHours(nextBusinessDay, startWorkingHours, endWoringHours);
        }

        private static DateTime GetNextBusinessDay(DateTime calculationDateTime, decimal numberOfDays, List<DateTime> holidays) 
        {
            DateTime businessDayWithoutWeekend = GetBusinessDayWithoutWeekend(calculationDateTime, numberOfDays);
            return Strategies.Context.Strategy.AdditionalDaysForHolidays(calculationDateTime, businessDayWithoutWeekend, holidays);
        }

        private static DateTime GetBusinessDayWithoutWeekend(DateTime calculationDateTime, decimal numberOfDays) 
        { 
            DateTime potentialEndDate = CalculateEndDateFromNumberOfDays(calculationDateTime, numberOfDays);

            int additionaDays = 0;
            
            int numberOfWeeks = Convert.ToInt32((calculationDateTime - potentialEndDate).TotalDays / 7);
            additionaDays += numberOfWeeks * 2;

            int remainingDays = additionaDays != 0 ? Convert.ToInt32(numberOfDays % 7) : Convert.ToInt32(numberOfDays);

            return Strategies.Context.Strategy.AdditionalDaysForWeekend(potentialEndDate, remainingDays);
        }

        private static DateTime CalculateEndDateFromNumberOfDays(DateTime calculationDateTime, decimal numberOfDays) 
        {
            if (Double.TryParse(numberOfDays.ToString(), out double days))
            {
                return calculationDateTime.AddDays(days);
            }
            else
            {
                throw new ArgumentException($"Exception while parsing the value  value [{numberOfDays}]");
            }
        }

        public static bool IsWeekendDay(DayOfWeek dayOfWeek) 
        {
            return dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday;
        }
    }
}
