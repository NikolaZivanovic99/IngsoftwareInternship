using MovieLibrary.Business.ViewModels;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;

namespace MovieLibrary.Business.Services.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<List<MovieViewModel>> GetMovies();
        Task AddMovie(MovieViewModel movieModel,string pathRoot);
        Task UpdateMovie(MovieViewModel movieModel,string pathRoot);
        Task<MovieViewModel> GetMovie(int id);
        Task DeleteMovie(int? id,string pathRoot);
        Task<List<MovieViewModel>> SearchMovie(string movieSearch,int genresSearch);
    }
}
