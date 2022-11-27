using Application.Abstractions.Messaging;

namespace Application.Movies.Commands.CreateMovie
{
    public sealed record CreateMovieCommand(string Name, DateTime ReleasedOn) : ICommand<Guid>;
}
