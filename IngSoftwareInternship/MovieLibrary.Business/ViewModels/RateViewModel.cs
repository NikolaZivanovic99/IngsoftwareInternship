using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.ViewModels
{
    public class RateViewModel
    {
        public RateViewModel()
        {
            Movies = new HashSet<MovieViewModel>();
        }
        public int RateId { get; set; }
        [Range(1, 10)]
        [Required(ErrorMessage = "Rate is required!")]
        public int Rates { get; set; }
        [Required(ErrorMessage = "Comment is required!")]
        [StringLength(300, MinimumLength = 4)]
        public string Comment { get; set; } = null!;
        public DateTime? InsertDate { get; set; }
        public string UserId { get; set; } = null!;
        public int MovieId { get; set; }
        public virtual UserViewModel? User { get; set; } = null!;
        public ICollection<MovieViewModel> Movies { get; set; }
    }
}
