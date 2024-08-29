using FluentValidation;
using SocialMedia.Application.Internationalization;
using SocialMedia.Application.Posts.CreatePost;

namespace SocialMedia.Application.Posts.CreateStory;

public class CreateStoryCommandValidator : AbstractValidator<CreateStoryCommand>
{
    public CreateStoryCommandValidator(ILanguageResource resource)
    {
        RuleFor(f => f.Image)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(resource.FieldRequired(nameof(CreateStoryCommand.Image)));
    }
}