namespace TicketSystem.Domain.Common.Utilities;

public class NamedItem<T>
{
    public T Id { get; set; }
    public string Name { get; set; }

    public NamedItem(T id, string name)
    {
        Id = id;
        Name = name;
    }

    public NamedItem()
    {
    }
}

public class NamedItemString : NamedItem<string>
{
    public NamedItemString(string id, string name) : base(id, name)
    {
    }

    public NamedItemString()
    {
    }
}

public class NamedItemInt : NamedItem<int>
{
    public NamedItemInt(int id, string name) : base(id, name)
    {
    }

    public NamedItemInt()
    {
    }
}

public class NamedItemGuid : NamedItem<Guid>
{
    public NamedItemGuid(Guid id, string name) : base(id, name)
    {
    }

    public NamedItemGuid()
    {
    }
}

public class NamedItemEnum<T> : NamedItem<T> where T : Enum
{
    public NamedItemEnum()
    {
    }

    public NamedItemEnum(T @enum)
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