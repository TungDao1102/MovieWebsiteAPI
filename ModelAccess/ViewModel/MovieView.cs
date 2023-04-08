using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAccess.ViewModel
{
    public class MovieView
    {
        public int MovieId { get; set; }

        public int? TypeId { get; set; }

        public string MovieName { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }

        public string? PosterPath { get; set; }

        public double? Duration { get; set; }

        public double? AverageRating { get; set; }

        public long? ViewCount { get; set; }

        public string? OverView { get; set; }
        public bool IsDelete { get; set; }
    }
}
