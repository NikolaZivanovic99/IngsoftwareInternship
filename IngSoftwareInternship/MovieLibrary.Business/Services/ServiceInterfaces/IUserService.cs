using MovieLibrary.Business.ViewModels;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;

namespace MovieLibrary.Business.Services.ServiceInterfaces
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetUsers();
        Task AddUser(UserViewModel user);
        Task<UserViewModel> GetUser(int id);
        Task UpdateUser(UserViewModel user);
        Task DeleteUser(int? id);
        Task<List<OccupationViewModel>> GetOccupations();
    }
}
