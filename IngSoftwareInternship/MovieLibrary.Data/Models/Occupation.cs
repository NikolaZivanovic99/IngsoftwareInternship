using System;
using System.Collections.Generic;

namespace MovieLibrary.Data.Modelss
{
    public partial class Occupation
    {
        public Occupation()
        {
            Users = new HashSet<User>();
        }

        public int OccupationId { get; set; }
        public string Caption { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
