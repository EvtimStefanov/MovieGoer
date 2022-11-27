namespace Application.Movies.Queries.GetMovieById
{
    public sealed record MovieResponse(Guid Id, string Name, DateTime ReleasedOn);
}
