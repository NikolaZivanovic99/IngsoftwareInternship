using MovieLibrary.Business.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.ServiceInterface
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
