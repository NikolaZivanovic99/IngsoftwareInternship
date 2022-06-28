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
            User user = _mapper.Map<User>(userModel);
            user.InsertDate = DateTime.Now;
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void DeleteUser(int? id)
        {
            User user = _context.Users.Find(id);
            user.DeleteDate = DateTime.Now;
            _context.SaveChanges();
        }

        public List<OccupationViewModel> GetOccupations()
        {
            List<OccupationViewModel> occupations = _mapper.Map<List<OccupationViewModel>>(_context.Occupations.ToList()); 
            return occupations;
        }

        public UserViewModel GetUser(int id)
        {
            UserViewModel user =_mapper.Map<UserViewModel>( _context.Users.Include(x => x.Occupation).Where(c => c.UserId == id && c.DeleteDate == null).FirstOrDefault());       
            return user;    
        }
        public List<UserViewModel> GetUsers()
        {
            List<UserViewModel> userViewModels = _mapper.Map<List<UserViewModel>>(_context.Users.Include(c => c.Occupation).Where(x => x.DeleteDate == null).ToList());
            return userViewModels;
        }
        public void UpdateUser(UserViewModel userModel)
        {
            User user = _mapper.Map<User>(userModel);
            User userFromDataBase = _context.Users.Find(user.UserId);
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
