using MovieLibrary.Business.ServiceInterface;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.Service
{
    public class MovieService : IMovieService
    {
        MoviesDataBaseContext _context;

        public MovieService(MoviesDataBaseContext context)
        {
           _context = context;
        }

        public Movie AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return movie;
        }

        public void DeleteMovie(int? id)
        {
            Movie movie = _context.Movies.Find(id);
            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }

        public Movie GetMovie(int id)
        {
            Movie movie = _context.Movies.Find(id);
            return movie;
        }

        public List<Movie> GetMovies() 
        {
            List<Movie> movies = _context.Movies.ToList();
            return movies;
        }

        public Movie UpdateMovie(Movie movie)
        {
            Movie movieFromDataBase = _context.Movies.Find(movie.MovieId);
            movieFromDataBase.Caption = movie.Caption;
            movieFromDataBase.ReleaseYear = movie.ReleaseYear;
            movieFromDataBase.SubmittedBy = movie.SubmittedBy;
            movieFromDataBase.InsertDate = movie.InsertDate;
            movieFromDataBase.DeleteDate = movie.DeleteDate;

            _context.SaveChanges();
            return movie;

        }
    }
}
