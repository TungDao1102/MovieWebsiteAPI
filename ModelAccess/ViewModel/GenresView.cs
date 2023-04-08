using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAccess.ViewModel
{
    public class GenresView
    {
        public int GenresId { get; set; }
        public string GenresName { get; set; } = null!;
        public bool IsDelete { get; set; }  
    }
}
