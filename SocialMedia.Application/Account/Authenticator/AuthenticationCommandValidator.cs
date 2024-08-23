using FluentValidation;
using MediatR;
using SocialMedia.Application.Internationalization;

namespace SocialMedia.Application.Account.Authenticator;

public class AuthenticationCommandValidator : AbstractValidator<AuthenticationCommand>
{
    private readonly IMediator _mediator;
    public AuthenticationCommandValidator(IMediator mediator, ILanguageResource resource)
    {
        _mediator = mediator;
        RuleFor(c => c.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(resource.FieldRequired(nameof(AuthenticationCommand.Email)))
            .MustAsync(EmailExist).WithMessage(resource.InvalidEmail());
        RuleFor(c => c.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(resource.FieldRequired(nameof(AuthenticationCommand.Password)));
    }
    
    private async Task<bool> EmailExist(AuthenticationCommand request, string email, CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(new GetUserByEmailQuery
        {
            Email = email
        }, cancellationToken);

        return user != null;
    }
}