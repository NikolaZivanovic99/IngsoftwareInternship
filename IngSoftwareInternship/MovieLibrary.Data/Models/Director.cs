using System;
using System.Collections.Generic;

namespace MovieLibrary.Data.Models
{
    public partial class Director
    {
        public Director()
        {
            Movies = new HashSet<Movie>();
        }

        public int DirectorId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string ImagePath { get; set; } = null!;

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
