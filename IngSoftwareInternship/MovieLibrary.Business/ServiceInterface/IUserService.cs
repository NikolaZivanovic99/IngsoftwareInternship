using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.ServiceInterface
{
    public interface IUserService
    {
        List<User> GetUsers();
        User AddUser(User user);
        User GetUser(int id);
        User UpdateUser(User user);
        void DeleteUser(int? id);
    }
}
