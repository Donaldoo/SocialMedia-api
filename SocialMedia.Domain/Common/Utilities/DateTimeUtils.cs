namespace TicketSystem.Domain.Common.Utilities;

public static class DateTimeUtils
{
    public static DateTime DateFromMilliseconds(long milliseconds) =>
        (new DateTime(1970, 1, 1)).AddMilliseconds(milliseconds);

    public static DateTime FirstDayOfMonth(DateTime dateTime) =>
        new DateTime(dateTime.Year, dateTime.Month, 1).Date;
    
    public static bool MonthsAreEqual(DateTime date1, DateTime date2) =>
         date1.Month == date2.Month && date1.Year == date2.Year;

    public static DateTime LastDayOfYear(DateTime dateTime) =>
        new DateTime(dateTime.Year, 12, 31).Date;

    public static DateTime LastDayOfYear(int year) =>
        new DateTime(year, 12, 31).Date;

    public static DateTime FirstDayOfYear(DateTime dateTime) =>
        new DateTime(dateTime.Year, 1, 1).Date;

    public static DateTime FirstDayOfYear(int year) =>
        new DateTime(year, 1, 1).Date;

    public static DateTime LastDayOfMonth(DateTime dateTime) =>
        FirstDayOfMonth(dateTime).AddMonths(1).AddDays(-1).Date;

    public static DateTime NextFirstDayOfMonth(DateTime dateTime) =>
        FirstDayOfMonth(dateTime).AddMonths(1).Date;

    public static DateTime NextLastDayOfMonth(DateTime dateTime) =>
        NextFirstDayOfMonth(dateTime).AddMonths(1).AddDays(-1).Date;

    public static DateTime Min(DateTime date1, DateTime date2) =>
        new[] { date1, date2 }.Min();

    public static DateTime Max(DateTime date1, DateTime date2) =>
        new[] { date1, date2 }.Max();

    public static int CalculateAge(DateTime birthDate)
    {
        var today = DateTime.Today;

        var age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age)) age--;

        return age;
    }
}

public static class DateTimeExtensions
{
    public static string ToShortDateTimeString(this DateTime date) =>
        $"{date.ToShortDateString()} {date.ToShortTimeString()}";

    public static string ToDateTimeInSecondsString(this DateTime date) =>
        $"{date:yyyy-MM-dd_HH-mm-ss}";

    public static long ToMilliseconds(this DateTime date) =>
        ((DateTimeOffset)date.ToUniversalTime()).ToUnixTimeSeconds();

    public static bool IsNullOrEmpty(this DateTime? date) =>
        !date.HasValue || date.Value.Date.Equals(DateTime.MinValue.Date);

    public static bool IsEmpty(this DateTime date) =>
        date.Date.Equals(DateTime.MinValue.Date);

    public static int MonthsPast(this DateTime date, DateTime otherDate)
    {
        if (otherDate > date)
            return 0;

        return (date.Year - otherDate.Year) * 12 + (date.Month - otherDate.Month);
    }

    public static bool IsMinDate(this DateTime? date)
    {
        return date.HasValue && date.Value.IsMinDate();
    }

    public static bool IsMinDate(this DateTime date)
    {
        return date.Day == 1 && date.Month == 1 && date.Year == 0001;
    }
}

public static class DateTimeOffsetExtensions
{
    public static bool IsNullOrEmpty(this DateTimeOffset? date) =>
        !date.HasValue || date.Value.Date.Equals(DateTimeOffset.MinValue.Date);

    public static bool IsEmpty(this DateTimeOffset date) =>
        date.Date.Equals(DateTimeOffset.MinValue.Date);

    public static int MonthsPast(this DateTimeOffset date, DateTimeOffset otherDate)
    {
        if (otherDate > date)
            return 0;

        return (date.Year - otherDate.Year) * 12 + (date.Month - otherDate.Month);
    }

    public static bool IsMinDate(this DateTimeOffset? date)
    {
        return date.HasValue && date.Value.Date.IsMinDate();
    }
}