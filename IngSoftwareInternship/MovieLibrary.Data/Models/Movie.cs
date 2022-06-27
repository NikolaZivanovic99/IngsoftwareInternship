using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.Data.Models
{
    public partial class Movie
    {
        public int MovieId { get; set; }
        public string Caption { get; set; } = null!;
        [Display(Name ="Release Year")]
        public int ReleaseYear { get; set; }
        [Display(Name ="Submitted by")]
        public string SubmittedBy { get; set; } = null!;
        public DateTime InsertDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
