namespace WorkdayNet.Models
{
    public class RecurringHoliday
    {
        public RecurringHoliday(int _month, int _day) 
        {
            Month = _month;
            Day = _day;
        }

        public int Month { get; set; }
        public int Day { get; set; }
    }
}
