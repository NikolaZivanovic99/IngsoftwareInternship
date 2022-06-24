using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;

namespace MovieLibrary.Business.ServiceInterface
{
    public interface IMovieService
    {
        List<Movie> GetMovies();
        void AddMovie(Movie movie);
        void UpdateMovie(Movie movie);
        Movie GetMovie(int id);
        void DeleteMovie(int? id);
    }
}
