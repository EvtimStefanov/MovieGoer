using Domain.Abstractions;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public sealed class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MovieRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Insert(Movie movie) => _dbContext.Set<Movie>().Add(movie);
    }
}
