using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAccess.ViewModel
{
    public class ActorView
    {
        public int ActorId { get; set; }

        public string? ActorName { get; set; }

        public string? Avartar { get; set; }
        public bool IsDelete { get; set; }
    }
}
