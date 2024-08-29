using FluentValidation;
using SocialMedia.Application.Internationalization;
using SocialMedia.Application.Posts.CreateLike;

namespace SocialMedia.Application.Posts.CreateComment;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator(ILanguageResource resource)
    {
        RuleFor(x => x.PostId)
            .NotEmpty().WithMessage(resource.FieldRequired(nameof(CreateCommentCommand.PostId)));
        
        RuleFor(x => x.Description)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(resource.FieldRequired(nameof(CreateCommentCommand.Description)))
            .MaximumLength(400).WithMessage("Comment cannot exceed 400 characters");
    }
}