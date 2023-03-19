using System;
using System.Collections.Generic;

namespace APIWebMovie.Models;

public partial class ReView
{
    public int ReviewId { get; set; }

    public int UserId { get; set; }

    public int MovieId { get; set; }

    public DateTime Date { get; set; }

    public double Rate { get; set; }

    public string Content { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
