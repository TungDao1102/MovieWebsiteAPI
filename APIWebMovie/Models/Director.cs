using System;
using System.Collections.Generic;

namespace APIWebMovie.Models;

public partial class Director
{
    public int DirectorId { get; set; }

    public string DirectorName { get; set; } = null!;
    public bool IsDelete { get; set; }

    public virtual ICollection<DetailDirectorMovie> DetailDirectorMovies { get; } = new List<DetailDirectorMovie>();
}
