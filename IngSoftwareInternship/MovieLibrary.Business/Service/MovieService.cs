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
        public async Task AddMovie(MovieViewModel movieModel, int[] directors, int[]genres)
        {
            Movie movie= _mapper.Map<Movie>(movieModel);
            movie.Directors = await _context.Directors.Where(r => directors.Contains(r.DirectorId)).ToListAsync();
            movie.Genres = await _context.Genres.Where(x => genres.Contains(x.GenreId)).ToListAsync(); 
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
            return _mapper.Map<List<MovieViewModel>>(movies); ;
        }
        public async Task UpdateMovie(MovieViewModel movieModel, int[] directors, int[] genres)
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
            ICollection<Director> movieDirectors = await _context.Directors.Where(r => directors.Contains(r.DirectorId)).ToListAsync();
            ICollection<Genre> movieGenres = await _context.Genres.Where(x => genres.Contains(x.GenreId)).ToListAsync();

            movieFromDataBase.Caption = movie.Caption;
            movieFromDataBase.ReleaseYear = movie.ReleaseYear;
            movieFromDataBase.SubmittedBy = movie.SubmittedBy;
            movieFromDataBase.InsertDate = DateTime.Now;
            movieFromDataBase.Directors = AddEntityDirector(movieFromDataBase.Directors,movieDirectors);
            movieFromDataBase.Genres = AddEntityGenre(movieFromDataBase.Genres, movieGenres);
           await _context.SaveChangesAsync();
        }

        private ICollection<Director> DeleteEntityDirector(ICollection<Director> movieFromDataBase, ICollection<Director> selectedDirectors) 
        {
            foreach (var item2 in movieFromDataBase)
            {
                if (!selectedDirectors.Contains(item2))
                {
                    movieFromDataBase.Remove(item2);
                }
            }
            return movieFromDataBase;
        }
        private ICollection<Director> AddEntityDirector(ICollection<Director> movieFromDataBase, ICollection<Director> selectedDirectors)
        {
            foreach (var item2 in selectedDirectors)
            {
                if (!movieFromDataBase.Contains(item2))
                {
                    movieFromDataBase.Add(item2);
                }
            }
            movieFromDataBase = DeleteEntityDirector(movieFromDataBase, selectedDirectors);
            return movieFromDataBase;
        }

        private ICollection<Genre> DeleteEntityGenre(ICollection<Genre> movieFromDataBase, ICollection<Genre> selectedGenres)
        {
            foreach (var item2 in movieFromDataBase)
            {
                if (!selectedGenres.Contains(item2))
                {
                    movieFromDataBase.Remove(item2);
                }
            }
            return movieFromDataBase;
        }
        private ICollection<Genre> AddEntityGenre(ICollection<Genre> movieFromDataBase, ICollection<Genre> selectedGenres)
        {
            foreach (var item2 in selectedGenres)
            {
                if (!movieFromDataBase.Contains(item2))
                {
                    movieFromDataBase.Add(item2);
                }
            }
            movieFromDataBase = DeleteEntityGenre(movieFromDataBase, selectedGenres);
            return movieFromDataBase;
        }
    }
}
