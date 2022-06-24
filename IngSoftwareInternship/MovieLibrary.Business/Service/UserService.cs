using MovieLibrary.Business.ServiceInterface;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;


namespace MovieLibrary.Business.Service
{
    public class UserService : IUserService
    {
        private readonly MoviesDataBaseContext _context;
        public UserService(MoviesDataBaseContext context)
        {
            _context = context;
        }
        public void AddUser(User user)
        {
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

        public List<Occupation> GetOccupations()
        {
            List<Occupation> occupations = _context.Occupations.ToList(); 
            return occupations;
        }

        public User GetUser(int id)
        {
            User user = _context.Users.Find(id);
            user.Occupation = _context.Occupations.Find(user.OccupationId);         
            return user;    
        }
        public List<User> GetUsers()
        {
            List<User> users = _context.Users.Where(x=> x.DeleteDate == null).ToList();
            foreach (User user in users) 
            {
                user.Occupation = _context.Occupations.Find(user.OccupationId);                    
            }
            return users;
        }
        public void UpdateUser(User user)
        {
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
