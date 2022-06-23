using MovieLibrary.Business.ServiceInterface;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.Service
{
    public class UserService : IUserService
    {
        MoviesDataBaseContext _context;
        public UserService(MoviesDataBaseContext context)
        {
            _context = context;
        }

        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public void DeleteUser(int? id)
        {
            User user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public User GetUser(int id)
        {
            User user = _context.Users.Find(id);
          
            return user;    

        }
        public List<User> GetUsers()
        {
            List<User> users = _context.Users.ToList();
            Occupation occupation = new Occupation();
            foreach (User user in users) 
            {
                occupation = _context.Occupations.Find(user.OccupationId);
                user.Occupation.OccupationId = occupation.OccupationId;
                user.Occupation.Caption = occupation.Caption;
                    
            }
            return users;
        }

        public User UpdateUser(User user)
        {
            User userFromDataBase = _context.Users.Find(user.UserId);
            userFromDataBase.FirstName = user.FirstName;
            userFromDataBase.LastName = user.LastName;
            userFromDataBase.Address = user.Address;
            userFromDataBase.Idnumber = user.Idnumber;
            userFromDataBase.OccupationId = user.OccupationId;
            userFromDataBase.InsertDate = user.InsertDate;
            userFromDataBase.DeleteDate = user.DeleteDate;

            _context.SaveChanges();
            return user;
        }
    }
}
