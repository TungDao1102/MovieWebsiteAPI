using System;
using System.Collections.Generic;

namespace APIWebMovie.Models;

public partial class DetailGenresMovie
{
    public int Id { get; set; }

    public int MovieId { get; set; }

    public int GenresId { get; set; }

    public virtual Genre Genres { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
