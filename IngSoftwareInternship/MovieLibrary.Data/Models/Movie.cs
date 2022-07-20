using System;
using System.Collections.Generic;

namespace MovieLibrary.Data.Models
{
    public partial class Movie
    {
        public Movie()
        {
            Directors = new HashSet<Director>();
            Genres = new HashSet<Genre>();
            Users = new HashSet<ApplicationUser>();
            Rates = new HashSet<Rate>();
        }

        public int MovieId { get; set; }
        public string Caption { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public string SubmittedBy { get; set; } = null!;
        public DateTime InsertDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string ImagePath { get; set; } = null!;

        public virtual ICollection<Director> Directors { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
    }
}
