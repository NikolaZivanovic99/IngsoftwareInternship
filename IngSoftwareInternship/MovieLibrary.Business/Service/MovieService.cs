using AutoMapper;
using MovieLibrary.Business.ServiceInterface;
using MovieLibrary.Business.ViewModel;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;

namespace MovieLibrary.Business.Service
{
    public class MovieService : IMovieService
    {
        private readonly MoviesDataBaseContext _context;
        private readonly IMapper _mapper;
        public MovieService(MoviesDataBaseContext context, IMapper mapper)
        {
           _context = context;
            _mapper = mapper;  
        }
        public void AddMovie(MovieViewModel movieModel)
        {
            Movie movie = _mapper.Map<Movie>(movieModel);
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
        public MovieViewModel GetMovie(int id)
        {
            Movie movie = _context.Movies.Find(id);
            if (movie == null || movie.DeleteDate !=null)
            {
                return null;
            }
            return _mapper.Map<MovieViewModel>(movie);
        }

        public List<MovieViewModel> GetMovies() 
        {
            return _mapper.Map<List<MovieViewModel>>(_context.Movies.Where(x=> x.DeleteDate ==null).ToList());
        }
        public void UpdateMovie(MovieViewModel movieModel)
        {
            Movie movie = _mapper.Map<Movie>(movieModel);
            Movie movieFromDataBase = _context.Movies.Find(movie.MovieId);
            movieFromDataBase.Caption = movie.Caption;
            movieFromDataBase.ReleaseYear = movie.ReleaseYear;
            movieFromDataBase.SubmittedBy = movie.SubmittedBy;
            movieFromDataBase.InsertDate = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
