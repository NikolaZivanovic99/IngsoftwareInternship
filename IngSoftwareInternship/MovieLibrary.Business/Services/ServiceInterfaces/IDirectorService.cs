using MovieLibrary.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.Services.ServiceInterfaces
{
    public interface IDirectorService
    {
        Task<List<DirectorViewModel>> GetDirectors();
        Task AddDirector(DirectorViewModel directorModel,string pathRoot);
        Task UpdateDirector(DirectorViewModel directorModel,string pathRoot);
        Task<DirectorViewModel> GetDirector(int id);
        Task DeleteDirector(int? id,string pathRoot);
    }
}
