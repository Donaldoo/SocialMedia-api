using System.Reflection;

namespace TicketSystem.Domain.Common.Attributes;

public static class AttributesUtils
{
    public static IEnumerable<Type> GetTypesWithAttribute(Assembly assembly, Type type)
    {
        return from assemblyType in assembly.GetTypes()
            where assemblyType.GetCustomAttributes(type, true).Length > 0
            select assemblyType;
    }
}