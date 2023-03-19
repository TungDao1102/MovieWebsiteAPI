namespace APIWebMovie.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IReviewRepository reviewRepository { get; }
        IUserRepository userRepository { get; }
        IDirectorRepository directorRepository { get; }
        IGenresRepository genresRepository { get; }
        IMovieRepository movieRepository { get; }
        IActorRepository actorRepository { get; }
        ITeaserRepository teaserRepository { get; }
        ITypeMovieRepository typeMovieRepository { get; }
        IDetailActorMovieRepository detailActorMovieRepository { get; }
        IDetailGenresMovieRepository detailGenresMovieRepository { get; }
        IDetailDirectorMovieRepository detailDirectorMovieRepository { get; }
        IDetailUserMovieFavoriteRepository detailUserMovieFavoriteRepository { get; }

        int Save();
    }
}
