using FluentValidation;
using SocialMedia.Application.Internationalization;

namespace SocialMedia.Application.ChatHub.CreateChat;

public class CreateChatCommandValidator : AbstractValidator<CreateChatCommand>
{
    public CreateChatCommandValidator(ILanguageResource resource)
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage(resource.FieldRequired(nameof(CreateChatCommand.UserId)));
    }
}