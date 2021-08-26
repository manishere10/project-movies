using Movies.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Contract
{
    public interface IMovieService
    {
        Task<MovieEntity> Get(long id);

        Task<int> Create(MovieEntity movie);

        Task<List<MovieEntity>> GetAll(int pageNumber);

    }
}
