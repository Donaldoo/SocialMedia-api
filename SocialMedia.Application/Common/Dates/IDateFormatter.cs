using System.Globalization;

namespace SocialMedia.Application.Common.Dates;

public interface IDateFormatter
{
    public DateOnly NewDate(string date);
    public string FormatDate(DateOnly date);
}

public class DateFormatter : IDateFormatter
{
    public DateOnly NewDate(string date) => DateOnly.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
    public string FormatDate(DateOnly date) => date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
}