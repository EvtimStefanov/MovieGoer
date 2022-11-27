using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IMovieRepository
    {
        void Insert(Movie moive);
    }
}
