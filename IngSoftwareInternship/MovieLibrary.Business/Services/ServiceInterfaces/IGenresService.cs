using MovieLibrary.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.Services.ServiceInterfaces
{
    public interface IGenresService
    {
        Task<List<GenreViewModel>> GetGenres();
        Task AddGenre(GenreViewModel genreModel);
        Task UpdateGenre(GenreViewModel genreModel);
        Task<GenreViewModel> GetGenre(int id);
        Task DeleteGenre(int? id);
    }
}
