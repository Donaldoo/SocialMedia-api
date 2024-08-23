namespace TicketSystem.Domain.Common.Utilities;

public static class EnvironmentUtils
{
    public static string GetEnvironmentValue(string name, string defaultValue) =>
        Environment.GetEnvironmentVariable(name) ?? defaultValue;
}