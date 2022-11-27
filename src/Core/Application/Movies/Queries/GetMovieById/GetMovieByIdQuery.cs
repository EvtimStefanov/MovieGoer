using Application.Abstractions.Messaging;

namespace Application.Movies.Queries.GetMovieById
{
    public sealed record GetMovieByIdQuery(Guid movieId) : IQuery<MovieResponse>;
}
