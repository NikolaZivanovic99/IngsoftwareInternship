using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            Movie movie = await _context.Movies.Include(c => c.Directors).Include(d => d.Genres).FirstOrDefaultAsync(x => x.DeleteDate == null);
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
        public async Task UpdateMovie(MovieViewModel movieModel, int[] directors, int[]genres)
        {
            Movie movie = _mapper.Map<Movie>(movieModel);
            Movie movieFromDataBase = await _context.Movies.FindAsync(movie.MovieId);
            if (movieFromDataBase == null)
            {
                throw new ValidationException("The movie with the given ID does not exist. Please try again!");
            }
            if (movieFromDataBase.Caption != movieModel.Caption) 
            {
                Movie movieCheck = await _context.Movies.Where(x=> x.Caption == movieModel.Caption).FirstOrDefaultAsync();
                if (movieCheck != null) 
                {
                    throw new ValidationException("A movie with that caption already exists. Please try again");               
                }
            }
            movieFromDataBase.Caption = movie.Caption;
            movieFromDataBase.ReleaseYear = movie.ReleaseYear;
            movieFromDataBase.SubmittedBy = movie.SubmittedBy;
            movieFromDataBase.InsertDate = DateTime.Now;
            movieFromDataBase.Directors = await _context.Directors.Where(r => directors.Contains(r.DirectorId)).ToListAsync();
            movieFromDataBase.Genres = await _context.Genres.Where(x => genres.Contains(x.GenreId)).ToListAsync();

            await _context.SaveChangesAsync();
        }
    }
}
