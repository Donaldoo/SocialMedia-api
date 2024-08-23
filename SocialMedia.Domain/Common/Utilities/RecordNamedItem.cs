namespace TicketSystem.Domain.Common.Utilities;

public record RecordNamedItem<T>
{
    public T Id { get; init; }
    public string Name { get; init; }

    public RecordNamedItem(T id, string name)
    {
        Id = id;
        Name = name;
    }

    public RecordNamedItem()
    {
    }
}

public record RecordNamedItemString : RecordNamedItem<string>
{
    public RecordNamedItemString(string id, string name) : base(id, name)
    {
    }

    public RecordNamedItemString()
    {
    }
}

public record RecordNamedItemInt : RecordNamedItem<int>
{
    public RecordNamedItemInt(int id, string name) : base(id, name)
    {
    }

    public RecordNamedItemInt()
    {
    }
}

public record RecordNamedItemGuid : RecordNamedItem<Guid>
{
    public RecordNamedItemGuid(Guid id, string name) : base(id, name)
    {
    }

    public RecordNamedItemGuid()
    {
    }
}

public record RecordNamedItemEnum<T> : RecordNamedItem<T> where T : Enum
{
    public RecordNamedItemEnum()
    {
    }

    public RecordNamedItemEnum(T @enum)
    {
        Id = @enum;
        Name = @enum.ToDescriptionString();
    }

    public static IList<NamedItemEnum<T>> CreateList()
    {
        var list = EnumUtils.EnumToList<T>()
            .Select(c => new NamedItemEnum<T>(c))
            .ToList();
        return list;
    }
}