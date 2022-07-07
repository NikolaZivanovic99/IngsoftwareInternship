using MovieLibrary.Business.ViewModel;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;

namespace MovieLibrary.Business.ServiceInterface
{
    public interface IMovieService
    {
        Task<List<MovieViewModel>> GetMovies();
        Task AddMovie(MovieViewModel movieModel);
        Task UpdateMovie(MovieViewModel movieModel);
        Task<MovieViewModel> GetMovie(int id);
        Task DeleteMovie(int? id);
        Task<List<MovieViewModel>> SearchMovie(string movieSearch);
        Task<List<MovieViewModel>> SearchGenres(int genresId);
    }
}
