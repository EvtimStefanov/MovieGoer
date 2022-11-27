using Application.Movies.Commands.CreateMovie;
using FluentValidation;

public sealed class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(255)
            .Must((command) => !command.Contains('@'));

        RuleFor(x => x.ReleasedOn).NotEmpty();
    }
}