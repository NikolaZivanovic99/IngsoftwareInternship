using MovieLibrary.Business.ViewModel;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;

namespace MovieLibrary.Business.ServiceInterface
{
    public interface IMovieService
    {
        Task<List<MovieViewModel>> GetMovies();
        Task AddMovie(MovieViewModel movieModel, int[] directors, int[] genres);
        Task UpdateMovie(MovieViewModel movieModel, int[] directors, int[]genres);
        Task<MovieViewModel> GetMovie(int id);
        Task DeleteMovie(int? id);
    }
}
