using System;
using System.Collections.Generic;

namespace APIWebMovie.Models;

public partial class TypeMovie
{
    public int TypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Movie> Movies { get; } = new List<Movie>();
}
