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
        public int MovieId { get; set; }
        public string Caption { get; set; } = null!;
        [Display(Name = "Release Year")]
        public int ReleaseYear { get; set; }
        [Display(Name = "Submitted by")]
        public string SubmittedBy { get; set; } = null!;
    }
}
