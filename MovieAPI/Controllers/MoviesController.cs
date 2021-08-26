using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movies.Contract;
using Movies.Models;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;

        private readonly ILogger<MoviesController> _logger;

        public MoviesController(ILogger<MoviesController> logger, IMovieService movieService)
        {
            this._logger = logger;
            this.movieService = movieService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var movieEntity = await this.movieService.Get(id);

                if (movieEntity != null)
                {
                    return Ok(movieEntity);
                }

                return NoContent();

            }
            catch (System.Exception ex)
            {
                this._logger.LogError("Error occured in MoviesController.Get - {0}", ex.Message);
                return BadRequest("Something went wrong.");
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] MovieEntity movie)
        {
            try
            {
                if (movie.Title != null)
                {
                    var response = await this.movieService.Create(movie);
                    return Ok("Movie added successfully");
                }
                else
                {
                    return BadRequest("Title can not be null.");
                }
            }
            catch (System.Exception ex)
            {
                this._logger.LogError("Error occured in MoviesController.Get - {0}", ex.Message);
                return BadRequest("Something went wrong.");
            }
        }

        [HttpGet("{pageNumber}")]
        public async Task<IActionResult> GetAll(int pageNumber)
        {
            try
            {
                var movieList = this.movieService.GetAll(pageNumber);
                return Ok(movieList);
            }
            catch (System.Exception ex)
            {
                this._logger.LogError("Error occured in MoviesController.Get - {0}", ex.Message);
                return BadRequest("Something went wrong.");
            }
        }
    }
}