namespace SocialMedia.Application.Account;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyHashedPassword(string hashedPassword, string providedPassword);
}

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return Crypto.HashPassword(password);
    }

    public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        return Crypto.VerifyHashedPassword(hashedPassword, providedPassword);
    }
}