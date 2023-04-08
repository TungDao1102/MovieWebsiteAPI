using System;
using System.Collections.Generic;

namespace APIWebMovie.Models;

public partial class Actor
{
    public int ActorId { get; set; }

    public string? ActorName { get; set; }

    public string? Avartar { get; set; }
    public bool IsDelete { get; set; }

    public virtual ICollection<DetailActorMovie> DetailActorMovies { get; } = new List<DetailActorMovie>();
}
