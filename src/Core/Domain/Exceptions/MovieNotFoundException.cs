using Domain.Exceptions.Base;

namespace Domain.Exceptions
{
    public class MovieNotFoundException : NotFoundException
    {
        public MovieNotFoundException(Guid movieId)
            : base($"The movie with identifier {movieId} was not found")
        {
        }
    }
}
