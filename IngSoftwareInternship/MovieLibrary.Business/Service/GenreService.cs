using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Business.ServiceInterface;
using MovieLibrary.Business.ViewModel;
using MovieLibrary.Data.Models;
using MovieLibrary.Data.Modelss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.Service
{
    public class GenreService : IGenresService
    {
        private readonly MoviesDataBaseContext _context;
        private readonly IMapper _mapper;
        public GenreService(MoviesDataBaseContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddGenre(GenreViewModel genreModel)
        {
            Genre genre = _mapper.Map<Genre>(genreModel);
            genre.InsertDate=DateTime.Now;
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGenre(int? id)
        {
            Genre genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                throw new ValidationException("The genre with the given ID does not exist. Please try again!");
            }
            genre.DeleteDate = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task<GenreViewModel> GetGenre(int id)
        {
            Genre genre = await _context.Genres.FindAsync(id);
            if (genre == null || genre.DeleteDate != null)
            {
                throw new ValidationException("The ganre with the given ID does not exist. Please try again!");
            }
            return _mapper.Map<GenreViewModel>(genre);
        }

        public async Task<List<GenreViewModel>> GetGenres()
        {
            var genres = await _context.Genres.Where(x => x.DeleteDate == null).ToListAsync();
            return _mapper.Map<List<GenreViewModel>>(genres);
        }

        public async Task UpdateGenre(GenreViewModel genreModel)
        {
            Genre genre = _mapper.Map<Genre>(genreModel);
            Genre genreFromDataBase = await _context.Genres.FindAsync(genre.GenreId);
            if (genreFromDataBase == null)
            {
                throw new ValidationException("The genre with the given ID does not exist. Please try again!");
            }
            if (genreFromDataBase.Caption != genreModel.Caption)
            {
                Genre genreCheck = await _context.Genres.Where(x => x.Caption == genreModel.Caption).FirstOrDefaultAsync();
                if (genreCheck != null)
                {
                    throw new ValidationException("A genre with that caption already exists. Please try again");
                }
            }
            genreFromDataBase.Caption = genre.Caption;
            genreFromDataBase.Description = genre.Description;
            genreFromDataBase.InsertDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }
    }
}
