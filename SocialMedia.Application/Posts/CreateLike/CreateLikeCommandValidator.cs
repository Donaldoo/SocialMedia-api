using FluentValidation;
using SocialMedia.Application.Internationalization;

namespace SocialMedia.Application.Posts.CreateLike;

public class CreateLikeCommandValidator : AbstractValidator<CreateLikeCommand>
{
    public CreateLikeCommandValidator(ILanguageResource resource)
    {
        RuleFor(x => x.PostId)
            .NotEmpty().WithMessage(resource.FieldRequired(nameof(CreateLikeCommand.PostId)));
    }
}