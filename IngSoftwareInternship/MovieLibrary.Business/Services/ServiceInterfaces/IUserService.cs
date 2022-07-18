using MovieLibrary.Business.ViewModels;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;

namespace MovieLibrary.Business.Services.ServiceInterfaces
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetUsers();
        Task<UserViewModel> GetUser(string id);
        Task UpdateUser(UserViewModel user);
        Task DeleteUser(string? id);
        Task<List<OccupationViewModel>> GetOccupations();
        Task DeleteMovie(int movieId, string userId);
    }
}
