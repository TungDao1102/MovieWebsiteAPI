using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAccess.ViewModel
{
    public class ReviewView
    {
        public int UserId { get; set; }

        public int MovieId { get; set; }

        public DateTime Date { get; set; }

        public double Rate { get; set; }

        public string Content { get; set; } = null!;
    }
}
