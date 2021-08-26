using Microsoft.Extensions.Logging;
using Movies.Contract;
using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class MovieService : IMovieService
    {
        private readonly ILogger<MovieService> _logger;

        public IEnumerable<MovieEntity> Movies { get; set; }

        public MovieService(ILogger<MovieService> logger)
        {
            this._logger = logger;

            Movies = new List<MovieEntity>
                    {
                    new MovieEntity { Id = 1, Title = "Toy Story 1", Director = "John Lasseter" },
                    new MovieEntity { Id = 2, Title = "Toy Story 4", Director = "Josh Cooley" },
                    new MovieEntity { Id = 3, Title = "Arrival", Director = "Denis Villeneuve" },
                    new MovieEntity { Id = 4, Title = "Interstellar", Director = "Christopher Nolan" },
                    new MovieEntity { Id = 5, Title = "The Martian", Director = "Ridley Scott" },
                    new MovieEntity { Id = 6, Title = "Avatar", Director = "James Cameron" },
                    new MovieEntity { Id = 7, Title = "Prometheus", Director = "Ridley Scott" },
                    new MovieEntity { Id = 8, Title = "Sunshine", Director = "Danny Boyle" },
                    new MovieEntity { Id = 9, Title = "Serenity", Director = "Joss Whedon" },
                    new MovieEntity { Id = 10, Title = "WALL-E", Director = "Andrew Stanton" },
                    new MovieEntity { Id = 11, Title = "WALL-E", Director = "Andrew Stanton" },
                    new MovieEntity { Id = 12, Title = "WALL-E", Director = "Andrew Stanton" },
            };
        }
        public async Task<int> Create(MovieEntity movie)
        {
            try
            {
                if (movie.Title != null)
                {
                    Movies.ToList().Add(movie);
                }

                return movie.Id;
            }
            catch (System.Exception ex)
            {
                this._logger.LogError("Error occured in MoviesService.Create - {0}", ex.Message);
                return 0;
            }
        }

        public async Task<MovieEntity> Get(long id)
        {
            try
            {
                var movie = Movies.FirstOrDefault(item => item.Id == id);
                return movie;
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error occured in MoviesService.Get - {0}", ex.Message);
                throw;
            }
        }

        public async Task<List<MovieEntity>> GetAll(int pageNumber)
        {
            try
            {
                var movieList = Movies.ToList().Skip((pageNumber - 1) * 10).Take(10);
                return movieList.ToList();
            }
            catch (System.Exception ex)
            {
                this._logger.LogError("Error occured in MoviesController.Get - {0}", ex.Message);
                return null;
            }
        }
    }
}
