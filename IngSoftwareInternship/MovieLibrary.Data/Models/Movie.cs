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
        }

        public int MovieId { get; set; }
        public string Caption { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public string SubmittedBy { get; set; } = null!;
        public DateTime InsertDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public virtual ICollection<Director> Directors { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
    }
}
