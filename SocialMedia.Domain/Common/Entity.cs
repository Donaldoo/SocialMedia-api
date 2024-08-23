namespace SocialMedia.Domain.Common;

public record Entity<T> : IEntity<T>
{
    public T Id { get; set; }

    public virtual bool Equals(Entity<T>  other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        if (other.GetType() != this.GetType()) return false;
        return EqualityComparer<T>.Default.Equals(Id, other.Id);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<T>.Default.GetHashCode(Id);
    }
}