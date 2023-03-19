using System;
using System.Collections.Generic;

namespace APIWebMovie.Models;

public partial class DetailDirectorMovie
{
    public int Id { get; set; }

    public int MovieId { get; set; }

    public int DirectorId { get; set; }

    public virtual Director Director { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
