using System;
using System.Collections.Generic;

namespace APIWebMovie.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string PassWord { get; set; } = null!;

    public string UserType { get; set; } = null!;

    public virtual ICollection<Bill> Bills { get; } = new List<Bill>();

    public virtual ICollection<DetailUserMovieFavorite> DetailUserMovieFavorites { get; } = new List<DetailUserMovieFavorite>();

    public virtual ICollection<ReView> ReViews { get; } = new List<ReView>();
}
