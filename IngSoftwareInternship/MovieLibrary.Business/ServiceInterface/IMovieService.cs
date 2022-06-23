using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.ServiceInterface
{
    public interface IMovieService
    {
        List<Movie> GetMovies();
        Movie AddMovie(Movie movie);
        Movie UpdateMovie(Movie movie);
        Movie GetMovie(int id);
        void DeleteMovie(int? id);
    }
}
