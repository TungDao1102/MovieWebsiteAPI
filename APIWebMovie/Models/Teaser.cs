using System;
using System.Collections.Generic;

namespace APIWebMovie.Models;

public partial class Teaser
{
    public int TeaserId { get; set; }

    public int MovieId { get; set; }

    public string Name { get; set; } = null!;

    public string Key { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
