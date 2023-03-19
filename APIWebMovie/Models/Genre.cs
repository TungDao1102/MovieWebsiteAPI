using System;
using System.Collections.Generic;

namespace APIWebMovie.Models;

public partial class Genre
{
    public int GenresId { get; set; }

    public string GenresName { get; set; } = null!;

    public virtual ICollection<DetailGenresMovie> DetailGenresMovies { get; } = new List<DetailGenresMovie>();
}
