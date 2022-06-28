using MovieLibrary.Business.ViewModel;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;

namespace MovieLibrary.Business.ServiceInterface
{
    public interface IMovieService
    {
        List<MovieViewModel> GetMovies();
        void AddMovie(MovieViewModel movieModel);
        void UpdateMovie(MovieViewModel movieModel);
        MovieViewModel GetMovie(int id);
        void DeleteMovie(int? id);
    }
}
