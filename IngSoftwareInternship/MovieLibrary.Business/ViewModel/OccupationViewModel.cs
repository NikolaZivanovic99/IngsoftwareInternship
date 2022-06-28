using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.ViewModel
{
    public class OccupationViewModel
    {
        public OccupationViewModel()
        {
            Users = new HashSet<UserViewModel>();
        }

        public int OccupationId { get; set; }
        public string Caption { get; set; } = null!;

        public virtual ICollection<UserViewModel> Users { get; set; }
    }
}
