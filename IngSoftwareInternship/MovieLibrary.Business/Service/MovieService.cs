using MovieLibrary.Business.ServiceInterface;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;

namespace MovieLibrary.Business.Service
{
    public class MovieService : IMovieService
    {
        private readonly MoviesDataBaseContext _context;
        public MovieService(MoviesDataBaseContext context)
        {
           _context = context;
        }
        public void AddMovie(Movie movie)
        {
            movie.InsertDate = DateTime.Now;
            _context.Movies.Add(movie);
            _context.SaveChanges();
        }
        public void DeleteMovie(int? id)
        {
            Movie movie = _context.Movies.Find(id);
            movie.DeleteDate = DateTime.Now;
            _context.SaveChanges();
        }
        public Movie GetMovie(int id)
        {
            Movie movie = _context.Movies.Find(id);
            if (movie == null || movie.DeleteDate !=null)
            {
                return null;
            }
            return movie;
        }

        public List<Movie> GetMovies() 
        {
            return _context.Movies.Where(x=> x.DeleteDate ==null).ToList();
        }
        public void UpdateMovie(Movie movie)
        {
            Movie movieFromDataBase = _context.Movies.Find(movie.MovieId);
            movieFromDataBase.Caption = movie.Caption;
            movieFromDataBase.ReleaseYear = movie.ReleaseYear;
            movieFromDataBase.SubmittedBy = movie.SubmittedBy;
            movieFromDataBase.InsertDate = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
