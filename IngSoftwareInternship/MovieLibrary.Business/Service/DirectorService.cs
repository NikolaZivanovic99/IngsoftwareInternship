using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Business.ServiceInterface;
using MovieLibrary.Business.ViewModel;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.Service
{
    public class DirectorService : IDirectorService
    {
        private readonly MoviesDataBaseContext _context;
        private readonly IMapper _mapper;
        public DirectorService(MoviesDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddDirector(DirectorViewModel directorModel)
        {
            Director director = _mapper.Map<Director>(directorModel);
            director.InsertDate = DateTime.Now;
            _context.Directors.Add(director);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDirector(int? id)
        {
            Director director = await _context.Directors.FindAsync(id);
            if (director == null)
            {
                throw new ValidationException("The Director with the given ID does not exist. Please try again!");
            }
            director.DeleteDate = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task<DirectorViewModel> GetDirector(int id)
        {
            Director director = await _context.Directors.FindAsync(id);
            if (director == null || director.DeleteDate != null)
            {
                throw new ValidationException("The Director with the given ID does not exist. Please try again!");
            }
            return _mapper.Map<DirectorViewModel>(director);
        }

        public async Task<List<DirectorViewModel>> GetDirectors()
        {
            var directors = await _context.Directors.Where(x => x.DeleteDate == null).ToListAsync();
            return _mapper.Map<List<DirectorViewModel>>(directors);
        }

        public async Task UpdateDirector(DirectorViewModel directorModel)
        {
            Director director = _mapper.Map<Director>(directorModel);
            Director directorFromDataBase = await _context.Directors.FindAsync(director.DirectorId);
            if (directorFromDataBase == null)
            {
                throw new ValidationException("The director with the given identifier does not exist. Please try again");
            }

            directorFromDataBase.FirstName = director.FirstName;
            directorFromDataBase.LastName = director.LastName;
            directorFromDataBase.DateOfBirth = director.DateOfBirth;
            directorFromDataBase.InsertDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }
    }
}
