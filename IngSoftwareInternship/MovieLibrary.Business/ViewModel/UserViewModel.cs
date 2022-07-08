using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.ViewModel
{
    public class UserViewModel
    {
       
        public int UserId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage ="First Name is required!")]
        [StringLength(50, MinimumLength =4)]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required!")]
        [StringLength(50, MinimumLength = 4)]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Address is required!")]
        [StringLength(512, MinimumLength = 4)]
        public string Address { get; set; } = null!;

        [Display(Name = "Id number")]
        [Required(ErrorMessage = "Id Number is required!")]
        [StringLength(50, MinimumLength = 4)]
        public string IdNumber { get; set; } = null!;

        [Display(Name ="Occupation")]
        public int OccupationId { get; set; }

        public List<OccupationViewModel>? Occuppations { get; set; } = null!;
     
        public virtual OccupationViewModel? Occupation { get; set; } = null!;
    }
}
