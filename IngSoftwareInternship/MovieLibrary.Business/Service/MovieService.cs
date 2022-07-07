using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Business.ServiceInterface;
using MovieLibrary.Business.ViewModel;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public async Task AddMovie(MovieViewModel movieModel)
        {
            Movie movie= _mapper.Map<Movie>(movieModel);
            movie.Directors = await _context.Directors.Where(r => movieModel.SelectedDirectors.Contains(r.DirectorId)).ToListAsync();
            movie.Genres = await _context.Genres.Where(x => movieModel.SelectedGenres.Contains(x.GenreId)).ToListAsync(); 
            movie.InsertDate = DateTime.Now;
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteMovie(int? id)
        {
            Movie movie = await _context.Movies.FindAsync(id);
            if (movie == null) 
            {
                throw new ValidationException("The movie with the given ID does not exist. Please try again!");
            }
            movie.DeleteDate = DateTime.Now;
            await _context.SaveChangesAsync();
        }
        public async Task<MovieViewModel> GetMovie(int id)
        {
            Movie movie = await _context.Movies.Where(x=> x.MovieId==id && x.DeleteDate==null).Include(c => c.Directors).Include(d => d.Genres).FirstOrDefaultAsync();
            if (movie == null || movie.DeleteDate!=null) 
            {
                throw new ValidationException("The movie with the given ID does not exist. Please try again!");
            }
            return _mapper.Map<MovieViewModel>(movie);
        }

        public async Task<List<MovieViewModel>> GetMovies() 
        {
            var movies = await _context.Movies.Include(c=> c.Directors).Include(d=> d.Genres).Where(x => x.DeleteDate == null).ToListAsync();                    
            return _mapper.Map<List<MovieViewModel>>(movies);
        }
        public async Task UpdateMovie(MovieViewModel movieModel)
        {
            Movie movie = _mapper.Map<Movie>(movieModel);
            Movie movieFromDataBase = await _context.Movies.Include(c => c.Directors).Include(d => d.Genres).FirstOrDefaultAsync(x => x.MovieId == movie.MovieId && x.DeleteDate == null);
            if (movieFromDataBase == null)
            {
                throw new ValidationException("The movie with the given ID does not exist. Please try again!");
            }
            if (movieFromDataBase.Caption != movieModel.Caption)
            {
                Movie movieCheck = await _context.Movies.Where(x => x.Caption == movieModel.Caption).FirstOrDefaultAsync();
                if (movieCheck != null)
                {
                    throw new ValidationException("A movie with that caption already exists. Please try again");
                }
            }
            ICollection<Director> movieDirectors = await _context.Directors.Where(r => movieModel.SelectedDirectors.Contains(r.DirectorId)).ToListAsync();
            ICollection<Genre> movieGenres = await _context.Genres.Where(x => movieModel.SelectedGenres.Contains(x.GenreId)).ToListAsync();

            movieFromDataBase.Caption = movie.Caption;
            movieFromDataBase.ReleaseYear = movie.ReleaseYear;
            movieFromDataBase.SubmittedBy = movie.SubmittedBy;
            movieFromDataBase.InsertDate = DateTime.Now;
            movieFromDataBase.Directors = AddMovieDirector(movieFromDataBase.Directors,movieDirectors);
            movieFromDataBase.Genres = AddMovieGenre(movieFromDataBase.Genres, movieGenres);
           await _context.SaveChangesAsync();
        }
        public async Task<List<MovieViewModel>> SearchMovie(string movieSearch)
        {
            List<Movie> movies = await _context.Movies.Include(c => c.Directors).Include(d => d.Genres).Where(x => x.Caption.Contains(movieSearch) && x.DeleteDate==null).ToListAsync();
            return _mapper.Map<List<MovieViewModel>>(movies);
        }
        public async Task<List<MovieViewModel>> SearchGenres(int genres)
        {
            Genre genress = await _context.Genres.Where(x => x.GenreId == genres).FirstOrDefaultAsync();
            List<Movie> movies = await _context.Movies.Include(c => c.Directors).Include(d => d.Genres).Where(x => x.Genres.Contains(genress) && x.DeleteDate==null).ToListAsync();
            return _mapper.Map<List<MovieViewModel>>(movies);
        }
        private ICollection<Director> DeleteMovieDirector(ICollection<Director> movieDirectors, ICollection<Director> selectedDirectors) 
        {
            foreach (var director in movieDirectors)
            {
                if (!selectedDirectors.Contains(director))
                {
                    movieDirectors.Remove(director);
                }
            }
            return movieDirectors;
        }
        private ICollection<Director> AddMovieDirector(ICollection<Director> movieDirectors, ICollection<Director> selectedDirectors)
        {
            foreach (var director in selectedDirectors)
            {
                if (!movieDirectors.Contains(director))
                {
                    movieDirectors.Add(director);
                }
            }
            movieDirectors = DeleteMovieDirector(movieDirectors, selectedDirectors);
            return movieDirectors;
        }

        private ICollection<Genre> DeleteMovieGenre(ICollection<Genre> movieGenres, ICollection<Genre> selectedGenres)
        {
            foreach (var genre in movieGenres)
            {
                if (!selectedGenres.Contains(genre))
                {
                    movieGenres.Remove(genre);
                }
            }
            return movieGenres;
        }
        private ICollection<Genre> AddMovieGenre(ICollection<Genre> movieGenres, ICollection<Genre> selectedGenres)
        {
            foreach (var genre in selectedGenres)
            {
                if (!movieGenres.Contains(genre))
                {
                    movieGenres.Add(genre);
                }
            }
            movieGenres = DeleteMovieGenre(movieGenres, selectedGenres);
            return movieGenres;
        }

        
    }
}
