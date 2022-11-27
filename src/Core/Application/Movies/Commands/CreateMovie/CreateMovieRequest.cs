namespace Application.Movies.Commands.CreateMovie
{
    public sealed record CreateMovieRequest(string? Name, DateTime ReleasedOn = default);
}
