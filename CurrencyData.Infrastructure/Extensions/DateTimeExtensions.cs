using System;

namespace CurrencyData.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime GetRecentWorkingDayDate(this in DateTime date) =>
            date.DayOfWeek switch
            {
                DayOfWeek.Saturday => date.AddDays(-1),
                DayOfWeek.Sunday => date.AddDays(-2),
                _ => date
            };
    }
}
