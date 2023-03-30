using System;
using System.Collections.Generic;

namespace APIWebMovie.Models;

public partial class DetailUserMovieFavorite
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int MovieId { get; set; }

    public virtual Movie Movie { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
