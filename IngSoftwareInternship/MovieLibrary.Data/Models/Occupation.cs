using System;
using System.Collections.Generic;

namespace MovieLibrary.Data.Models
{
    public partial class Occupation
    {
        public Occupation()
        {
            Users = new HashSet<ApplicationUser>();
        }

        public int OccupationId { get; set; }
        public string Caption { get; set; } = null!;

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
