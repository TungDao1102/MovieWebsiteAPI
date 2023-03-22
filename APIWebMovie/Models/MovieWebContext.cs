using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIWebMovie.Models;

public partial class MovieWebContext : DbContext
{
    public MovieWebContext()
    {
    }

    public MovieWebContext(DbContextOptions<MovieWebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<DetailActorMovie> DetailActorMovies { get; set; }

    public virtual DbSet<DetailDirectorMovie> DetailDirectorMovies { get; set; }

    public virtual DbSet<DetailGenresMovie> DetailGenresMovies { get; set; }

    public virtual DbSet<DetailUserMovieFavorite> DetailUserMovieFavorites { get; set; }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<ReView> ReViews { get; set; }

    public virtual DbSet<Teaser> Teasers { get; set; }

    public virtual DbSet<TypeMovie> TypeMovies { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MR_TUNG_PC\\TUNGDAO;Initial Catalog=MovieWeb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(a => a.ActorId);
            entity.ToTable("Actor");

            entity.Property(e => e.ActorId)
                .ValueGeneratedNever()
                .HasColumnName("ActorID");
            entity.Property(e => e.ActorName).HasMaxLength(25);
            entity.Property(e => e.Avartar).HasMaxLength(50);
            entity.Property(e => e.Story).HasMaxLength(25);
        });

        modelBuilder.Entity<DetailActorMovie>(entity =>
        {
            entity.HasKey(dam => dam.Id);
            entity.ToTable("DetailActorMovie");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ActorId).HasColumnName("ActorID");
            entity.Property(e => e.MovieId).HasColumnName("MovieID");

            entity.HasOne(d => d.Actor).WithMany(p => p.DetailActorMovies)
                .HasForeignKey(d => d.ActorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetailActor_Actor");

            entity.HasOne(d => d.Movie).WithMany(p => p.DetailActorMovies)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetailActor_Movie");
        });

        modelBuilder.Entity<DetailDirectorMovie>(entity =>
        {
            entity.HasKey(ddm => ddm.Id);
            entity.ToTable("DetailDirectorMovie");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DirectorId).HasColumnName("DirectorID");
            entity.Property(e => e.MovieId).HasColumnName("MovieID");

            entity.HasOne(d => d.Director).WithMany(p => p.DetailDirectorMovies)
                .HasForeignKey(d => d.DirectorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetailDirector_Director");

            entity.HasOne(d => d.Movie).WithMany(p => p.DetailDirectorMovies)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetailDirector_Movie");
        });

        modelBuilder.Entity<DetailGenresMovie>(entity =>
        {
            entity.HasKey(dgm => dgm.Id);
            entity.ToTable("DetailGenresMovie");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.GenresId).HasColumnName("GenresID");
            entity.Property(e => e.MovieId).HasColumnName("MovieID");

            entity.HasOne(d => d.Genres).WithMany(p => p.DetailGenresMovies)
                .HasForeignKey(d => d.GenresId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetailGenres_Genres");

            entity.HasOne(d => d.Movie).WithMany(p => p.DetailGenresMovies)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetailGenres_Movie");
        });

        modelBuilder.Entity<DetailUserMovieFavorite>(entity =>
        {
            entity.HasKey(dum => dum.Id);
            entity.ToTable("DetailUserMovieFavorite");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.MovieId).HasColumnName("MovieID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Movie).WithMany(p => p.DetailUserMovieFavorites)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetailUserMovieFavorite_Movie");

            entity.HasOne(d => d.User).WithMany(p => p.DetailUserMovieFavorites)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetailUserMovieFavorite_User");
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(d => d.DirectorId);
            entity.ToTable("Director");

            entity.Property(e => e.DirectorId)
                .ValueGeneratedNever()
                .HasColumnName("DirectorID");
            entity.Property(e => e.DirectorName).HasMaxLength(25);
            entity.Property(e => e.Story).HasMaxLength(100);
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenresId);

            entity.Property(e => e.GenresId)
                .ValueGeneratedNever()
                .HasColumnName("GenresID");
            entity.Property(e => e.GenresName).HasMaxLength(100);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(m => m.MovieId);
            entity.ToTable("Movie");

            entity.Property(e => e.MovieId)
                .ValueGeneratedNever()
                .HasColumnName("MovieID");
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.MovieName).HasMaxLength(150);
            entity.Property(e => e.OverView).HasMaxLength(200);
            entity.Property(e => e.PosterPath).HasMaxLength(100);
            entity.Property(e => e.ReleaseDate).HasColumnType("date");
            entity.Property(e => e.TypeId).HasColumnName("TypeID");
            entity.Property(e => e.UrlVideo)
                .HasMaxLength(50)
                .HasColumnName("Url_Video");

            entity.HasOne(d => d.Type).WithMany(p => p.Movies)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movie_TypeMovie");
        });

        modelBuilder.Entity<ReView>(entity =>
        {
            entity.HasKey(r => r.ReviewId);
            entity.ToTable("ReView");

            entity.Property(e => e.ReviewId)
                .ValueGeneratedNever()
                .HasColumnName("ReviewID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.MovieId).HasColumnName("MovieID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Movie).WithMany(p => p.ReViews)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Review_Movie");

            entity.HasOne(d => d.User).WithMany(p => p.ReViews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Review_User");
        });

        modelBuilder.Entity<Teaser>(entity =>
        {
            entity.HasKey(t => t.TeaserId);
            entity.ToTable("Teaser");

            entity.Property(e => e.TeaserId)
                .ValueGeneratedNever()
                .HasColumnName("TeaserID");
            entity.Property(e => e.Key).HasMaxLength(25);
            entity.Property(e => e.MovieId).HasColumnName("MovieID");
            entity.Property(e => e.Name).HasMaxLength(25);

            entity.HasOne(d => d.Movie).WithMany(p => p.Teasers)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Teaser_Movie");
        });

        modelBuilder.Entity<TypeMovie>(entity =>
        {
            entity.HasKey(tm => tm.TypeId);
            entity.HasKey(e => e.TypeId);

            entity.ToTable("TypeMovie");

            entity.Property(e => e.TypeId)
                .ValueGeneratedNever()
                .HasColumnName("TypeID");
            entity.Property(e => e.Name).HasMaxLength(25);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.UserId);
            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.PassWord).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(100);
            entity.Property(e => e.UserType).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
