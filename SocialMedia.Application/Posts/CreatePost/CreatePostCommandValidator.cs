using FluentValidation;
using SocialMedia.Application.Internationalization;

namespace SocialMedia.Application.Posts.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator(ILanguageResource resource)
    {
        RuleFor(f => f.Description)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(resource.FieldRequired(nameof(CreatePostCommand.Description)))
            .MaximumLength(400).WithMessage("Description cannot be longer than 400 characters.");
    }
}