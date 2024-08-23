namespace SocialMedia.Application.Common.Dates;

public interface IDateTimeFactory
{
    DateTimeOffset NowWithOffset();
    DateTimeOffset UtcNowWithOffset();
    DateTime Now();
    DateTime UtcNow();
    DateTime MaxValue();
    DateTime MinValue();
    DateTime GetDateForDayOfWeek(DateTime dt, DayOfWeek startOfWeek);
    public DateOnly DateNow();
    public TimeOnly TimeNow();
}

public class DateTimeFactory : IDateTimeFactory
{
    DateTimeOffset IDateTimeFactory.NowWithOffset() => DateTimeOffset.Now;
    public DateTimeOffset UtcNowWithOffset() => DateTimeOffset.UtcNow;
    DateTime IDateTimeFactory.Now() => DateTime.Now;
    public DateTime UtcNow() => DateTime.UtcNow;
    public DateTime MaxValue() => DateTime.MaxValue;
    public DateTime MinValue() => DateTime.MinValue;

    public DateTime GetDateForDayOfWeek(DateTime dt, DayOfWeek startOfWeek)
    {
        var diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.AddDays(-1 * diff).Date;
    }

    public DateOnly DateNow() => new DateOnly(DateTime.UtcNow.Year, DateTime.UtcNow.Month,DateTime.UtcNow.Day);
    public TimeOnly TimeNow() => new TimeOnly(DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, DateTime.UtcNow.Second);
}