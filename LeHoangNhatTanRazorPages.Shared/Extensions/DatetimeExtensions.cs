﻿namespace LeHoangNhatTanRazorPages.Shared.Extensions
{
    public static class DatetimeExtensions
    {
        public static DateTime GetCurrentTimeInTimeZone(this DateTime datetime, string timeZoneId)
        {
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(datetime, timeZone);
        }

        public static DateTime SEAsiaStandardTime(this DateTime datetime)
        {
            return datetime.GetCurrentTimeInTimeZone("SE Asia Standard Time");
        }
    }
}
