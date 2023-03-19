namespace APIWebMovie.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public int TypeId { get; set; }

    public string MovieName { get; set; } = null!;

    public DateTime ReleaseDate { get; set; }

    public string? PosterPath { get; set; }

    public double? Duration { get; set; }

    public double? AverageRating { get; set; }

    public long? ViewCount { get; set; }

    public string? OverView { get; set; }

    public string? Country { get; set; }

    public string? UrlVideo { get; set; }

    public virtual ICollection<DetailActorMovie> DetailActorMovies { get; } = new List<DetailActorMovie>();

    public virtual ICollection<DetailDirectorMovie> DetailDirectorMovies { get; } = new List<DetailDirectorMovie>();

    public virtual ICollection<DetailGenresMovie> DetailGenresMovies { get; } = new List<DetailGenresMovie>();

    public virtual ICollection<DetailUserMovieFavorite> DetailUserMovieFavorites { get; } = new List<DetailUserMovieFavorite>();

    public virtual ICollection<ReView> ReViews { get; } = new List<ReView>();

    public virtual ICollection<Teaser> Teasers { get; } = new List<Teaser>();

    public virtual TypeMovie Type { get; set; } = null!;
}
