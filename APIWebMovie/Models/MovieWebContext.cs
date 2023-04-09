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

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<DetailActorMovie> DetailActorMovies { get; set; }

    public virtual DbSet<DetailDirectorMovie> DetailDirectorMovies { get; set; }

    public virtual DbSet<DetailGenresMovie> DetailGenresMovies { get; set; }

    public virtual DbSet<DetailUserMovieFavorite> DetailUserMovieFavorites { get; set; }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<ReView> ReViews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MSI;Initial Catalog=MovieWeb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.ToTable("Actor");

            entity.Property(e => e.ActorId)
                .HasColumnName("ActorID");
            entity.Property(e => e.ActorName).HasMaxLength(25);
            entity.Property(e => e.Avartar).HasMaxLength(50);
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Idbill);

            entity.ToTable("Bill");

            entity.Property(e => e.Idbill).HasColumnName("IDBill");
            entity.Property(e => e.Amount).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Number).HasMaxLength(20);
            entity.Property(e => e.OrderId).HasMaxLength(50);
            entity.Property(e => e.PaymentId).HasMaxLength(50);
            entity.Property(e => e.TimePayment).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Bills)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bill_User");
        });

        modelBuilder.Entity<DetailActorMovie>(entity =>
        {
            entity.ToTable("DetailActorMovie");

            entity.Property(e => e.Id)
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
            entity.ToTable("DetailDirectorMovie");

            entity.Property(e => e.Id)
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
            entity.ToTable("DetailGenresMovie");

            entity.Property(e => e.Id)
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
            entity.ToTable("DetailUserMovieFavorite");

            entity.Property(e => e.Id)
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
            entity.HasKey(e => e.DirectorId);
            entity.ToTable("Director");

            entity.Property(e => e.DirectorId)
                .HasColumnName("DirectorID");
            entity.Property(e => e.DirectorName).HasMaxLength(25);
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
            entity.HasKey(e => e.MovieId);
            entity.ToTable("Movie");

            entity.Property(e => e.MovieId)
                .ValueGeneratedNever()
                .HasColumnName("MovieID");
            entity.Property(e => e.MovieName).HasMaxLength(150);
            entity.Property(e => e.PosterPath).HasMaxLength(100);
            entity.Property(e => e.ReleaseDate).HasColumnType("date");

        });

        modelBuilder.Entity<ReView>(entity =>
        {
            entity.ToTable("ReView");

            entity.Property(e => e.ReviewId)
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

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .HasColumnName("UserID");
            entity.Property(e => e.PassWord).HasMaxLength(20);
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
