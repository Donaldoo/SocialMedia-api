namespace SocialMedia.Domain.AppConfigurations;

public class AppConfiguration
{
    public string Key { get; set; }
    public string Value { get; set; }

    protected bool Equals(AppConfiguration other)
    {
        return Key == other.Key;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((AppConfiguration)obj);
    }

    public override int GetHashCode()
    {
        return (Key != null ? Key.GetHashCode() : 0);
    }
}
