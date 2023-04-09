using APIWebMovie.Services;

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
        ITypeMovieRepository typeMovieRepository { get; }
        IBillRepository billRepository { get; }
        IDetailActorMovieRepository detailActorMovieRepository { get; }
        IDetailGenresMovieRepository detailGenresMovieRepository { get; }
        IDetailDirectorMovieRepository detailDirectorMovieRepository { get; }
        IDetailUserMovieFavoriteRepository detailUserMovieFavoriteRepository { get; }
        int Save();
    }
}
