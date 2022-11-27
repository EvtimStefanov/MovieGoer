using Application.Movies.Commands.CreateMovie;
using Application.Movies.Queries.GetMovieById;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;

public sealed class MoviesController : ApiController
{
    /// <summary>
    /// Gets the movie with the specified identifier, if it exists.
    /// </summary>
    /// <param name="movieId">The movie identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The movie with the specified identifier, if it exists.</returns>
    [HttpGet("{movieId:guid}")]
    [ProducesResponseType(typeof(MovieResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMovie(Guid movieId, CancellationToken cancellationToken)
    {
        var query = new GetMovieByIdQuery(movieId);

        var movie = await Sender.Send(query, cancellationToken);

        return Ok(movie);
    }

    /// <summary>
    /// Creates a new movie based on the specified request.
    /// </summary>
    /// <param name="request">The create movie request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The identifier of the newly created movie.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMovie(
        [FromBody] CreateMovieRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateMovieCommand>();

        var movieId = await Sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetMovie), new { movieId }, movieId);
    }
}