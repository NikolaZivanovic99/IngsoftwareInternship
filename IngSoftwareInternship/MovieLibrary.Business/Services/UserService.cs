using AutoMapper;
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
        public UserService(MoviesDataBaseContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddUser(UserViewModel userModel)
        {
            User user = await _context.Users.Where(x => x.IdNumber == userModel.IdNumber).FirstOrDefaultAsync();
            if (user != null) 
            {
                throw new ValidationException("A user with the given identifier already exists. Please try again");
            }
            user = _mapper.Map<User>(userModel);
            user.InsertDate = DateTime.Now;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUser(int? id)
        {
            User user = await _context.Users.FindAsync(id);
            if (user == null) 
            {
                throw new ValidationException("The user with the given identifier does not exist.Please try again");
            }
            user.DeleteDate = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task<List<OccupationViewModel>> GetOccupations()
        {
            var occupations = await _context.Occupations.ToListAsync();
             return _mapper.Map<List<OccupationViewModel>>(occupations);
        }

        public async Task<UserViewModel> GetUser(int id)
        {
            var user =await _context.Users.Include(x => x.Occupation).Where(c => c.UserId == id && c.DeleteDate == null).FirstOrDefaultAsync();
            if (user == null) 
            {
                throw new ValidationException("The user with the given identifier does not exist. Please try again");
            }
            return _mapper.Map<UserViewModel>(user);    
        }
        public async Task<List<UserViewModel>> GetUsers()
        {
            var users = await _context.Users.Include(c => c.Occupation).Where(x => x.DeleteDate == null).ToListAsync();
            return _mapper.Map<List<UserViewModel>>(users);
        }
        public async Task UpdateUser(UserViewModel userModel)
        {
            User user = _mapper.Map<User>(userModel);
            User userFromDataBase = await _context.Users.FindAsync(user.UserId);
            if (userFromDataBase == null)
            {
                throw new ValidationException("The user with the given identifier does not exist. Please try again");
            }
            if (userFromDataBase.IdNumber != userModel.IdNumber) 
            {
                User userCheck = await _context.Users.Where(x => x.IdNumber == userModel.IdNumber).FirstOrDefaultAsync();
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
            userFromDataBase.InsertDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }
    }
}
