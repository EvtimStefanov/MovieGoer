using Application.Abstractions.Messaging;
using Dapper;
using Domain.Exceptions;
using System.Data;

namespace Application.Movies.Queries.GetMovieById
{
    internal sealed class GetMovieQueryHandler : IQueryHandler<GetMovieByIdQuery, MovieResponse>
    {
        private readonly IDbConnection _dbConnection;

        public GetMovieQueryHandler(IDbConnection dbConnection) => _dbConnection = dbConnection;


        public async Task<MovieResponse> Handle(
            GetMovieByIdQuery request,
            CancellationToken cancellationToken)
        {
            const string sql = @"SELECT * FROM ""Movies"" WHERE ""Id"" = @MovieId";

            var movie = await _dbConnection.QueryFirstOrDefaultAsync<MovieResponse>(
                sql,
                new { request.movieId });

            if (movie == null)
            {
                throw new MovieNotFoundException(request.movieId);
            }

            return movie;
        }
    }
}
