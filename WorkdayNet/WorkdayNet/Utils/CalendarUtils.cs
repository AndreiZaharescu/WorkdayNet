using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkdayNet.Constants;

namespace WorkdayNet.Utils
{
    public static class CalendarUtils
    {

        public static DateTime GetBusinesDays(DateTime calculationDateTime, decimal numberOfDays, List<DateTime> holidays, TimeSpan startWorkingHours, TimeSpan endWoringHours)
        {
            decimal noneWorkingDays = GetNumberOfDaysWithoutWeekend(calculationDateTime, numberOfDays);
            DateTime potentialEndDate = CalculatePotentialEndDate(calculationDateTime, noneWorkingDays);

            foreach (DateTime holiday in holidays)
            {
                if (holiday.Ticks > calculationDateTime.Ticks  && holiday.Ticks < potentialEndDate.Ticks && !IsWeekendDay(holiday.DayOfWeek))
                {
                    noneWorkingDays++;
                }
            }

            DateTime bussinesDay = CalculatePotentialEndDate(calculationDateTime, noneWorkingDays);

            double hours = (double)(noneWorkingDays - Math.Truncate(noneWorkingDays));
            TimeSpan potentialWorkingHours = bussinesDay.TimeOfDay.Add(bussinesDay.AddDays(hours).TimeOfDay);

            DateTime nextBussinesDay = bussinesDay;
            if (potentialWorkingHours > endWoringHours) 
            {
                TimeSpan nextDaysHours = potentialWorkingHours - endWoringHours;

                bussinesDay = bussinesDay.AddDays(1);
                nextBussinesDay = bussinesDay.Date + startWorkingHours.Add(nextDaysHours);
            }
            return nextBussinesDay;
        }

        public static decimal GetNumberOfDaysWithoutWeekend(DateTime calculationDateTime, decimal numberOfDays) 
        { 
            DateTime potentialEndDate = CalculatePotentialEndDate(calculationDateTime, numberOfDays);

            int additionaDays = 0;
            
            int numberOfWeeks = (int)(calculationDateTime - potentialEndDate).TotalDays / 7;
            additionaDays += numberOfWeeks * 2;

            int remainingDays = additionaDays != 0 ? (int)numberOfDays % 7 : (int)numberOfDays;

            if (numberOfDays > 0)
            {
                DateTime lastWeekDays = potentialEndDate.AddDays(remainingDays * -1);
                for (int i = 0; i <= remainingDays; i++)
                {
                    if (IsWeekendDay(lastWeekDays.DayOfWeek))
                    {
                        additionaDays++;
                    }
                    lastWeekDays = lastWeekDays.AddDays(1);
                }

                return numberOfDays + additionaDays;
            }
            else 
            {
                DateTime firstWeekDays = calculationDateTime.AddDays(remainingDays);
                for (int i = remainingDays; i <= 0 ; i++)
                {
                    if (IsWeekendDay(firstWeekDays.DayOfWeek))
                    {
                        additionaDays--;
                    }
                    firstWeekDays = firstWeekDays.AddDays(-1);
                }

                return numberOfDays + additionaDays;
            }

        }


        private static DateTime CalculatePotentialEndDate(DateTime calculationDateTime, decimal numberOfDays) 
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

        private static bool IsWeekendDay(DayOfWeek dayOfWeek) 
        {
            return dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday;
        }

    }
}
