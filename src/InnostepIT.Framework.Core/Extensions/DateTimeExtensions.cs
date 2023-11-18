namespace InnostepIT.Framework.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static int GetQuarter(this DateTime dateTime)
        {
            return (int) Math.Ceiling(dateTime.Month / 3.0M);
        }
        
        public static IEnumerable<DateTime> GetQuartersBetweenDates(this DateTime current, DateTime past)
        {
            var currentQuarter = current.GetQuarter();
            var lastQuarterEnd = new DateTime(current.Year, currentQuarter * 3, 1).AddMonths(-2).AddDays(-1);

            while (lastQuarterEnd > past)
            {
                yield return lastQuarterEnd;
                lastQuarterEnd = lastQuarterEnd.AddMonths(-3);
            }
        }
    }
}
