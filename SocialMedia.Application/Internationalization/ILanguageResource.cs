namespace SocialMedia.Application.Internationalization;

public interface ILanguageResource
{
    string EntityWithIdNotFound(string entityName, object id);
    string FieldRequired(string name);
    string UserEmailAlreadyExists();
    string PasswordMustContainUppercaseLetter();
    string PasswordMustContainLowercaseLetter();
    string PasswordMustContainDigit();
    string PasswordMustContainSpecialChar();
    string PasswordTooShort();
    string PasswordsDoNotMatch();
    string InvalidPassword();
    string InvalidEmail();
    string InvalidSetDate();
    string UserDoesNotExist();
    string PasswordUpdated();
    string PasswordNotUpdated();
    string VerificationTokenExpired();
    string InvalidFile();
    string ResetPasswordTokenExpired();
    string DescriptionTooLong();
    string InvalidValue(string name);
}