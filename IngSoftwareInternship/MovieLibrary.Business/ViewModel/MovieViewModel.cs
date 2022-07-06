using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.ViewModel
{
    public class MovieViewModel
    {

        public MovieViewModel()
        {
            Directors = new HashSet<DirectorViewModel>();
            Genres = new HashSet<GenreViewModel>();
        }
        public int MovieId { get; set; }
        [Required(ErrorMessage = "Caption is required!")]
        [StringLength(50, MinimumLength = 4)]
        public string Caption { get; set; } = null!;
        [Display(Name = "Release Year")]
        [Range(1960,2022)]
        [Required(ErrorMessage ="Release Year is required!")]
        public int ReleaseYear { get; set; }
        [Display(Name = "Submitted By")]
        [Required(ErrorMessage = "Submitted By is required!")]
        [StringLength(100, MinimumLength = 4)]
        public string SubmittedBy { get; set; } = null!;
        public virtual ICollection<DirectorViewModel> Directors { get; set; }
        public virtual ICollection<GenreViewModel> Genres { get; set; }

        public string DirectorCsv { get
            {
                return string.Join(",", Directors.Select(x=> string.Join(" ",x.FirstName,x.LastName)));
            } }
        public string GenresCsv
        {
            get
            {
                return string.Join(",", Genres.Select(x => string.Join(" ",x.Caption)));
            }
        }
    }
}
