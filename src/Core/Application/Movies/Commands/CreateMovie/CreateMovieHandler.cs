using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Movies.Commands.CreateMovie
{
    internal sealed class CreateMovieHandler : ICommandHandler<CreateMovieCommand, Guid>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUnitOfWork _unitOfWorkl;

        public CreateMovieHandler(IUnitOfWork unitOfWorkl, IMovieRepository movieRepository)
        {
            _unitOfWorkl = unitOfWorkl;
            _movieRepository = movieRepository;
        }
        
        public async Task<Guid> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = new Movie(Guid.NewGuid(), request.Name, request.ReleasedOn);

            _movieRepository.Insert(movie);

            await _unitOfWorkl.SaveChangesAsync(cancellationToken);

            return movie.Id;
        }
    }
}
