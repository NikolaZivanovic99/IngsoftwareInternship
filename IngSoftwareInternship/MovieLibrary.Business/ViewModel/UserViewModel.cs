using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.ViewModel
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        [Display(Name = "Id number")]
        public string IdNumber { get; set; } = null!;
        public int OccupationId { get; set; }
       
        public virtual OccupationViewModel Occupation { get; set; } = null!;
    }
}
