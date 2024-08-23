namespace SocialMedia.Application.Internationalization;

public class EnglishLanguageResource : ILanguageResource
{
    public string EntityWithIdNotFound(string entityName, object id) => $"{entityName} with id {id} not found.";
    public string FieldRequired(string name) => $"Field {name} is Required!";
    public string UserEmailAlreadyExists() => "A User with this Email already Exists!";
    public string PasswordMustContainUppercaseLetter() => "Password must contain one or more capital letters.";
    public string PasswordMustContainLowercaseLetter() => "Password must contain one or more lowercase letters.";
    public string PasswordMustContainDigit() => "Password must contain one or more digits.";
    public string PasswordMustContainSpecialChar() => "Password must contain one or more special characters.";
    public string PasswordTooShort() => "Password must be at least 8 characters.";
    public string PasswordsDoNotMatch() => "Your passwords must match.";
    public string InvalidPassword() => "Invalid Password";
    public string InvalidEmail() => "Invalid Email";
    public string UserDoesNotExist() => "User does not exist";
    public string PasswordUpdated() => "Password changed successfully!";
    public string PasswordNotUpdated() => "Something went wrong updating the password";
    public string InvalidFile() => "Invalid File";
    public string InvalidSetDate() => "Start date can not be greater than end date or equal to current date.";
    public string VerificationTokenExpired() => "Verification token expired.";
    public string ResetPasswordTokenExpired() => "Reset password token expired.";
    public string DescriptionTooLong() => "Description should be less than 400 characters";
    public string InvalidValue(string name) => $"{name} is not a valid value";
}