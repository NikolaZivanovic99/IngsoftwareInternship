using System;
using System.Collections.Generic;

namespace MovieLibrary.Data.Modelss
{
    public partial class Genre
    {
        public Genre()
        {
            Movies = new HashSet<Movie>();
        }

        public int GenreId { get; set; }
        public string Caption { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
