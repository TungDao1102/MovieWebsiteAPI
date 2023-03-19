namespace BTLonWebMovie.Models
{
    public class Movie
    {
        public int MovieId { get; set; }

        public int TypeId { get; set; }

        public string? MovieName { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string? PosterPath { get; set; }

        public double? Duration { get; set; }

        public double? AverageRating { get; set; }

        public long? ViewCount { get; set; }

        public string? OverView { get; set; }

        public string? Country { get; set; }

        public string? UrlVideo { get; set; }
    }
}
