using System.ComponentModel;
using System.Text;

namespace TicketSystem.Domain.Common.Utilities;

public static class MyEnumExtensions
{
    public static string ToDescriptionString(this Enum value)
    {
        var attributes = (DescriptionAttribute[]) value
            .GetType()
            .GetField(value.ToString())
            .GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
}

public static class EnumUtils
{
    public static string GetEnumNamesAsCsv<T>(IEnumerable<T> enums) where T : struct
    {
        var joiner = "";
        var result = new StringBuilder();
        foreach (var e in enums)
        {
            result.Append(joiner);
            var value = e as Enum;
            result.Append(value.ToDescriptionString());
            joiner = ", ";
        }

        return result.ToString();
    }

    public static IEnumerable<T> EnumToList<T>()
    {
        var enumType = typeof(T);

        if (enumType.BaseType != typeof(Enum))
            throw new ArgumentException("T must be of type System.Enum");

        var enumValArray = Enum.GetValues(enumType);
        var enumValList = new List<T>(enumValArray.Length);

        foreach (int val in enumValArray)
        {
            enumValList.Add((T) Enum.Parse(enumType, val.ToString()));
        }

        return enumValList;
    }

    public static IEnumerable<NamedItem<T>> GetEnumValuesWithDescriptions<T, TE>()
    {
        var enumType = typeof(TE);
        if (enumType.BaseType != typeof(Enum))
            throw new ArgumentException("T is not of type Enum.");

        var enumValList = new List<NamedItem<T>>();
        foreach (var e in Enum.GetValues(enumType))
        {
            var fieldInto = e.GetType().GetField(e.ToString());
            var attributes =
                (DescriptionAttribute[]) fieldInto.GetCustomAttributes(typeof(DescriptionAttribute), false);

            enumValList.Add(new NamedItem<T>()
            {
                Id = (T) e,
                Name = attributes.Length > 0 ? attributes[0].Description : e.ToString()
            });
        }

        return enumValList;
    }
}