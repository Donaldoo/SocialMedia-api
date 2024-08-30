using FluentValidation;
using SocialMedia.Application.Internationalization;

namespace SocialMedia.Application.ChatHub.SendMessage;

public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator(ILanguageResource resource)
    {
        RuleFor(x => x.ChatId)
            .NotEmpty()
            .WithMessage(resource.FieldRequired(nameof(CreateMessageCommand.ChatId)));
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage(resource.FieldRequired(nameof(CreateMessageCommand.Content)));
    }
}