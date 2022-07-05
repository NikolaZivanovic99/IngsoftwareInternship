using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.ViewModel
{
    public class DirectorViewModel
    {
        public DirectorViewModel()
        {
            Movies = new HashSet<MovieViewModel>();
        }

        public int DirectorId { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required!")]
        [StringLength(50, MinimumLength = 4)]
        public string FirstName { get; set; } = null!;
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required!")]
        [StringLength(50, MinimumLength = 4)]
        public string LastName { get; set; } = null!;
        [Display(Name = "Date of birth")]
        [Required(ErrorMessage = "Date of birth required!")]
        
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<MovieViewModel> Movies { get; set; }
    }
}
