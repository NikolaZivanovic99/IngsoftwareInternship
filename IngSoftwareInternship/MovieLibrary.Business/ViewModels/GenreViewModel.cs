using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.ViewModels
{
    public class GenreViewModel
    {
        public GenreViewModel()
        {
            Movies = new HashSet<MovieViewModel>();
        }

        public int GenreId { get; set; }
        [Required(ErrorMessage = "Caption is required!")]
        [StringLength(50, MinimumLength = 4)]
        public string Caption { get; set; } = null!;
        
        [StringLength(200, MinimumLength = 0)]
        public string? Description { get; set; }
        public virtual ICollection<MovieViewModel> Movies { get; set; }
    }
}
