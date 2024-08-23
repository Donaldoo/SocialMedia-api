namespace SocialMedia.Application.Common.Exceptions;

public class EntityNotFoundException : Exception
{
    public string EntityName { get; } = string.Empty;
        
    public EntityNotFoundException(string message) : base(message)
    {
    }

    public EntityNotFoundException(string entityName, string message) : base(message)
    {
        EntityName = entityName;
    }
        
    public EntityNotFoundException() : base("Entity not found on database.")
    {
    }

    public static void ThrowIfNull<T>(T field, string message)
    {
        if (field == null)
        {
            throw new EntityNotFoundException(message);
        }
    }
}