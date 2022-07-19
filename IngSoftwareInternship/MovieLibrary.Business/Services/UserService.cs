using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Business.Services.ServiceInterfaces;
using MovieLibrary.Business.ViewModels;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;

namespace MovieLibrary.Business.Services
{
    public class UserService : IUserService
    {
        private readonly MoviesDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(MoviesDataBaseContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task DeleteMovie(int movieId, string userId)
        {
            ApplicationUser user = _userManager.Users.Include(x=>x.Movies).Where(x => x.Id == userId).FirstOrDefault();
            if (user == null) 
            {
                throw new ValidationException("User does not exist");
            }
            Movie movie = _context.Movies.Where(x => x.MovieId == movieId).FirstOrDefault();
            user.Movies.Remove(movie);        
            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteUser(string? id)
        {
            ApplicationUser user = await _userManager.Users.Where(x=> x.Id==id && x.DeleteDate==null).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ValidationException("The user with the given identifier does not exist.Please try again");
            }
            user.DeleteDate = DateTime.Now;
            await _userManager.UpdateAsync(user);
        }

        public async Task<List<OccupationViewModel>> GetOccupations()
        {
            var occupations = await _context.Occupations.ToListAsync();
            return _mapper.Map<List<OccupationViewModel>>(occupations);
        }
        public async Task<UserViewModel> GetUser(string id)
        {
            var user = await _userManager.Users.Include(x=>x.Occupation).Include(x=>x.Movies).ThenInclude(x=>x.Directors).Include(x=>x.Movies).ThenInclude(y=>y.Genres).Where(x => x.Id == id && x.DeleteDate==null).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ValidationException("The user with the given identifier does not exist. Please try again");
            }
            return _mapper.Map<UserViewModel>(user);
        }
        public async Task<List<UserViewModel>> GetUsers()
        {
            var users = await _userManager.Users.Include(x=>x.Occupation).Where(x=>x.DeleteDate==null).ToListAsync();
            return _mapper.Map<List<UserViewModel>>(users);
        }
        public async Task UpdateUser(UserViewModel userModel)
        {
            ApplicationUser user = _mapper.Map<ApplicationUser>(userModel);
            ApplicationUser userFromDataBase = await _userManager.Users.Where(x=>x.Id==userModel.Id && x.DeleteDate==null).FirstOrDefaultAsync();
            if (userFromDataBase == null)
            {
                throw new ValidationException("The user with the given identifier does not exist. Please try again");
            }
            if (userFromDataBase.IdNumber != userModel.IdNumber)
            {
                ApplicationUser userCheck = await _userManager.Users.Where(x => x.IdNumber == userModel.IdNumber).FirstOrDefaultAsync();
                if (userCheck != null)
                {
                    throw new ValidationException("A user with the given identifier already exists. Please try again");
                }
            }
            userFromDataBase.FirstName = user.FirstName;
            userFromDataBase.LastName = user.LastName;
            userFromDataBase.Address = user.Address;
            userFromDataBase.IdNumber = user.IdNumber;
            userFromDataBase.OccupationId = user.OccupationId;
            await _userManager.UpdateAsync(userFromDataBase);
        }
    }
}
