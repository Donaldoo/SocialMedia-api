using FluentValidation;
using MediatR;
using SocialMedia.Application.Account.Authenticator;
using SocialMedia.Application.Internationalization;

namespace SocialMedia.Application.Account.CreateAccount;

public class CreateAccountValidator : AbstractValidator<CreateAccountCommand>
{
    private readonly IMediator _mediator;
    public CreateAccountValidator( IMediator mediator, ILanguageResource resource)
    {
        _mediator = mediator;
        RuleFor(f => f.FirstName)
            .NotEmpty().WithMessage(resource.FieldRequired(nameof(CreateAccountCommand.FirstName)));
        
        RuleFor(f => f.LastName)
            .NotEmpty().WithMessage(resource.FieldRequired(nameof(CreateAccountCommand.LastName)));
        
        RuleFor(f => f.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(resource.FieldRequired(nameof(CreateAccountCommand.Email)))
            .MustAsync(BeUnique)
            .WithMessage(resource.UserEmailAlreadyExists());

        RuleFor(f => f.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(resource.FieldRequired(nameof(CreateAccountCommand.Password)))
            .MinimumLength(6).WithMessage(resource.PasswordTooShort())
            .WithMessage(resource.PasswordMustContainSpecialChar());
    }
    
    private async Task<bool> BeUnique(string email, CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(new GetUserByEmailQuery
        {
            Email = email
        }, cancellationToken);
        
        return user == null;
    }
}