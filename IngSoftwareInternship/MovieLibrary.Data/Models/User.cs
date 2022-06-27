using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.Data.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        [Display(Name="First Name")]
        public string FirstName { get; set; } = null!;
        [Display(Name="Last Name")]
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        [Display(Name ="Id number")]
        public string IdNumber { get; set; } = null!;
        public int OccupationId { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public virtual Occupation Occupation { get; set; } = null!; 
    }
}
