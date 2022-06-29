using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Business.ServiceInterface;
using MovieLibrary.Business.ViewModel;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;


namespace MovieLibrary.Business.Service
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
        public void AddUser(UserViewModel userModel)
        {
            User user = _context.Users.Where(x => x.IdNumber == userModel.IdNumber).FirstOrDefault();
            if (user != null) 
            {
                throw new ValidationException();
            }
            user = _mapper.Map<User>(userModel);
            user.InsertDate = DateTime.Now;
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void DeleteUser(int? id)
        {
            User user = _context.Users.Find(id);
            if (user == null) 
            {
                throw new ValidationException();
            }
            user.DeleteDate = DateTime.Now;
            _context.SaveChanges();
        }

        public List<OccupationViewModel> GetOccupations()
        {
             return _mapper.Map<List<OccupationViewModel>>(_context.Occupations.ToList());
        }

        public UserViewModel GetUser(int id)
        {
            UserViewModel user =_mapper.Map<UserViewModel>( _context.Users.Include(x => x.Occupation).Where(c => c.UserId == id && c.DeleteDate == null).FirstOrDefault());
            if (user == null) 
            {
                throw new ValidationException();
            }
            return user;    
        }
        public List<UserViewModel> GetUsers()
        {
            return _mapper.Map<List<UserViewModel>>(_context.Users.Include(c => c.Occupation).Where(x => x.DeleteDate == null).ToList());
        }
        public void UpdateUser(UserViewModel userModel)
        {
            User user = _mapper.Map<User>(userModel);
            User userFromDataBase = _context.Users.Find(user.UserId);
            if (userFromDataBase == null)
            {
                throw new ValidationException();
            }
            if (userFromDataBase.IdNumber != userModel.IdNumber) 
            {
                User userProvera = _context.Users.Where(x => x.IdNumber == userModel.IdNumber).FirstOrDefault();
                if (userProvera != null) 
                {
                    throw new ValidationException();
                }
            }
            userFromDataBase.FirstName = user.FirstName;
            userFromDataBase.LastName = user.LastName;
            userFromDataBase.Address = user.Address;
            userFromDataBase.IdNumber = user.IdNumber;
            userFromDataBase.OccupationId = user.OccupationId;
            userFromDataBase.InsertDate = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
