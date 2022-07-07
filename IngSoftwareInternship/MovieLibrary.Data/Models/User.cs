using System;
using System.Collections.Generic;

namespace MovieLibrary.Data.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string IdNumber { get; set; } = null!;
        public int OccupationId { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public virtual Occupation Occupation { get; set; } = null!;
    }
}
