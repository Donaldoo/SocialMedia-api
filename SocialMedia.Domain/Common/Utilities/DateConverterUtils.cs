namespace TicketSystem.Domain.Common.Utilities;

public static class DateConverterUtils
{
    public static DateTimeOffset DateOnlyToDateTimeOffset(DateOnly date)
        => new(DateOnlyToDateTime(date));

    public static DateTime DateOnlyToDateTime(DateOnly date)
        => new(date.Year, date.Month, date.Day);

    public static DateOnly DateTimeToDateOnly(DateTime date)
        => new(date.Year, date.Month, date.Day);

    public static DateOnly DateTimeOffsetToDateOnly(DateTimeOffset date)
        => new(date.Year, date.Month, date.Day);
}