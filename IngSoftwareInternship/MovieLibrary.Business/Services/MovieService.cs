﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Business.ViewModels;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using MovieLibrary.Business.Helpers;
using MovieLibrary.Business.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;

namespace MovieLibrary.Business.Services
{
    public class MovieService : IMovieService
    {
        private readonly MoviesDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public MovieService(MoviesDataBaseContext context, IMapper mapper,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task AddMovie(MovieViewModel movieModel,string wwwRootPath)
        {
            Movie movie = _mapper.Map<Movie>(movieModel);
            movie.Directors = await _context.Directors.Where(r => movieModel.SelectedDirectors.Contains(r.DirectorId)).ToListAsync();
            movie.Genres = await _context.Genres.Where(x => movieModel.SelectedGenres.Contains(x.GenreId)).ToListAsync();
            movie.InsertDate = DateTime.Now;
            movie.ImagePath = ImageHelper.SaveImage(movieModel.Image, wwwRootPath);
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteMovie(int? id,string pathRoot)
        {
            Movie movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                throw new ValidationException("The movie with the given ID does not exist. Please try again!");
            }
            if (movie.ImagePath != null) 
            {
                ImageHelper.DeleteImage(pathRoot, movie.ImagePath);
            }
            movie.DeleteDate = DateTime.Now;
            await _context.SaveChangesAsync();
        }
        public async Task<MovieViewModel> GetMovie(int id)
        {
            Movie movie = await _context.Movies.Where(x => x.MovieId == id && x.DeleteDate == null).Include(c => c.Directors).Include(d => d.Genres).Include(x=>x.Rates).FirstOrDefaultAsync();
            if (movie == null || movie.DeleteDate != null)
            {
                throw new ValidationException("The movie with the given ID does not exist. Please try again!");
            }
            return _mapper.Map<MovieViewModel>(movie);
        }

        public async Task<List<MovieViewModel>> GetMovies()
        {
            var movies = await _context.Movies.Include(c => c.Directors).Include(d => d.Genres).Include(x=>x.Users).Include(x=>x.Rates).Where(x => x.DeleteDate == null).ToListAsync();
            return _mapper.Map<List<MovieViewModel>>(movies);
        }
        public async Task AddToWatchList(int? id,string userId)
        {
            var user = await _userManager.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            if (user == null) 
            {
                throw new ValidationException("User does not exist!");
            }
            user.Movies.Add(await _context.Movies.Where(x => x.MovieId == id).FirstOrDefaultAsync());
            await _userManager.UpdateAsync(user);
        }
        public async Task RateMovie(RateViewModel rateView)
        {
            Movie movie = await _context.Movies.Include(x => x.Rates).Where(x => x.MovieId == rateView.MovieId).FirstOrDefaultAsync();
            if (movie.Rates.Where(x => x.UserId == rateView.UserId).FirstOrDefault()==null)
            {
                Rate rate = _mapper.Map<Rate>(rateView);
                rate.InsertDate = DateTime.Now;
                _context.Rate.Add(rate);
                movie.Rates.Add(rate);
                movie.AvgRate = Helpers.RateHelper.AvgRate(_mapper.Map<List<RateViewModel>>(movie.Rates));
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException("You are rated this movie!");
            }
        }
        public async Task<List<MovieViewModel>> FindPopular()
        {
            DateTime filterDate = DateTime.Now.AddDays(-30);
            var movies = await _context.Movies.OrderByDescending(x => x.Rates.Count).Include(x => x.Rates.Where(x=>x.InsertDate>filterDate)).ToListAsync();
            foreach (var movie in movies) 
            {
                movie.AvgRate = Helpers.RateHelper.AvgRate(_mapper.Map<List<RateViewModel>>(movie.Rates));
            }
            movies.Take(10);
            return _mapper.Map<List<MovieViewModel>>(movies);
        }
        public async Task UpdateMovie(MovieViewModel movieModel,string wwwRootPath)
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
            if (movieModel.Image != null) 
            {
                movieFromDataBase.ImagePath = ImageHelper.SaveImage(movieModel.Image, wwwRootPath);
            }
            
            movieFromDataBase.Caption = movie.Caption;
            movieFromDataBase.ReleaseYear = movie.ReleaseYear;
            movieFromDataBase.SubmittedBy = movie.SubmittedBy;
            movieFromDataBase.InsertDate = DateTime.Now;
            movieFromDataBase.Directors = AddMovieDirector(movieFromDataBase.Directors, movieDirectors);
            movieFromDataBase.Genres = AddMovieGenre(movieFromDataBase.Genres, movieGenres);
            await _context.SaveChangesAsync();
        }
        public async Task<List<MovieViewModel>> SearchMovie(string movieSearch, int genreId)
        {
            var filteredMovies = new List<Movie>();
            if (!string.IsNullOrEmpty(movieSearch) && genreId > 0)
            {
                filteredMovies = await _context.Movies
                    .Include(movie => movie.Directors)
                    .Include(movie => movie.Genres)
                    .Where(movie => movie.DeleteDate == null &&
                    (movie.Caption.Contains(movieSearch) || movie.Directors.Where(director => (director.FirstName + " " + director.LastName).Contains(movieSearch)).Any())
                     && movie.Genres.Where(genre => genre.GenreId == genreId).Any())
               .ToListAsync();
            }
            else 
            {
                filteredMovies = await _context.Movies
                        .Include(movie => movie.Directors)
                        .Include(movie => movie.Genres)
                        .Where(movie => movie.DeleteDate == null &&
                        (movie.Caption.Contains(movieSearch) ||
                        movie.Directors.Where(director => (director.FirstName + " " + director.LastName).Contains(movieSearch)).Any()
                       || movie.Genres.Where(genre => genre.GenreId == genreId).Any()))
                       .ToListAsync();
            }
            return _mapper.Map<List<MovieViewModel>>(filteredMovies);
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
