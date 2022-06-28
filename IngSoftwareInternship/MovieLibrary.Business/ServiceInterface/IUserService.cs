using MovieLibrary.Business.ViewModel;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;

namespace MovieLibrary.Business.ServiceInterface
{
    public interface IUserService
    {
        List<UserViewModel> GetUsers();
        void AddUser(UserViewModel user);
        UserViewModel GetUser(int id);
        void UpdateUser(UserViewModel user);
        void DeleteUser(int? id);
        List<OccupationViewModel> GetOccupations();
    }
}
