using MovieLibrary.Business.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.ServiceInterface
{
    public interface IDirectorService
    {
        Task<List<DirectorViewModel>> GetDirectors();
        Task AddDirector(DirectorViewModel directorModel);
        Task UpdateDirector(DirectorViewModel directorModel);
        Task<DirectorViewModel> GetDirector(int id);
        Task DeleteDirector(int? id);
    }
}
