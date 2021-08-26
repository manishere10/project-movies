using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MovieAPI.Controllers;
using Movies.Contract;
using Movies.Models;
using ServiceLayer;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Movies.Api.Test
{
    public class MovieControllerTest
    {
        private readonly MoviesController moviesMoq;
        private readonly Mock<ILogger<MoviesController>> logger;
        private readonly Mock<IMovieService> mockService;

        public MovieControllerTest()
        {
            this.logger = new Mock<ILogger<MoviesController>>();
            this.mockService = new Mock<IMovieService>();
            this.moviesMoq = new MoviesController(this.logger.Object, mockService.Object);
        }

        [Fact]
        public async void Get_Result_Movie_When_Passed_Valid_Id()
        {
            this.mockService.Setup(item => item.Get(It.IsAny<long>())).ReturnsAsync(new MovieEntity { Id = 1 });
            var result = await this.moviesMoq.Get(1);
            var okResult = result as OkObjectResult;
            Assert.Equal((double)HttpStatusCode.OK, (double)okResult.StatusCode);
        }

        [Fact]
        public async void Create_Result_Ok_When_Successful_Creation()
        {
            var movie = new MovieEntity { Id = 11, Title = "Toy Story 1", Director = "John Lasseter" };
            this.mockService.Setup(item => item.Create(It.IsAny<MovieEntity>())).ReturnsAsync(It.IsAny<int>());
            var movieResponse = await this.moviesMoq.Create(movie);
            var okResult = movieResponse as OkObjectResult;
            Assert.Equal((double)HttpStatusCode.OK, (double)okResult.StatusCode);
        }

        [Fact]
        public async void Get_Result_As_Top_Movie_When_Passed_PageNumber()
        {
            this.mockService.Setup(item => item.GetAll(It.IsAny<int>())).ReturnsAsync(new List<MovieEntity>());
            var result = await this.moviesMoq.GetAll(1);
            var okResult = result as OkObjectResult;
            Assert.Equal((double)HttpStatusCode.OK, (double)okResult.StatusCode);
        }
    }
}