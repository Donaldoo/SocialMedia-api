namespace TicketSystem.Domain.Common.Utilities;

public static class IntegerGenerator
{
    public static int RandomNumber(int min = 1, int max = 100000000) => new Random().Next(min, max);
}