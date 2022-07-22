using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Data.Models
{
    public class Rate
    {
        
        public int RateId { get; set; }
        public int Rates { get; set; } 
        public string Comment { get; set; } = null!;
        public DateTime InsertDate { get; set; }
        public string UserId { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }=null!;

    }
}
