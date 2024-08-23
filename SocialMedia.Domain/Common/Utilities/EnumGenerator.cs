namespace TicketSystem.Domain.Common.Utilities;

public static class EnumGenerator
{
    public static T RandomEnumValue<T>()
    {
        var v = Enum.GetValues(typeof(T));
        return (T) v.GetValue(new Random().Next(v.Length));
    }
}