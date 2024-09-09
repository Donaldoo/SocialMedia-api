namespace SocialMedia.Infrastructure.Persistence.Common.Data.Common;

[System.AttributeUsage(System.AttributeTargets.Class)]
public class EfSeederAttribute : System.Attribute
{
    public bool Enabled = true;
}