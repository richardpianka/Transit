using System;

namespace Transit.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static int SecondsIntoDay(this DateTime value)
        {
            return value.Hour*60*60 + value.Minute*60 + value.Second;
        }
    }
}
