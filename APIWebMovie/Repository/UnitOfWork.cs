using APIWebMovie.Interface;
using APIWebMovie.Models;

namespace APIWebMovie.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieWebContext _context;
        public UnitOfWork(MovieWebContext context)
        {
            _context = context;
            reviewRepository = new ReviewRepository(context);
            userRepository = new UserRepository(context);
            actorRepository = new ActorRepository(context);
            genresRepository = new GenresRepository(context);
            movieRepository = new MovieRepository(context);
            directorRepository = new DirectorRepository(context);
            teaserRepository = new TeaserRepository(context);
            typeMovieRepository = new TypeMovieRepository(context);
            detailActorMovieRepository = new DetailActorMovieRepository(context);
            detailGenresMovieRepository = new DetailGenresMovieRepository(context);
            detailDirectorMovieRepository = new DetailDirectorMovieRepository(context);
            detailUserMovieFavoriteRepository = new DetailUserMovieFavoriteRepository(context);
        }

        public IReviewRepository reviewRepository { get; }

        public IUserRepository userRepository { get; }

        public IDirectorRepository directorRepository { get; }

        public IGenresRepository genresRepository { get; }

        public IMovieRepository movieRepository { get; }

        public IActorRepository actorRepository { get; }
        public ITeaserRepository teaserRepository { get; }

        public ITypeMovieRepository typeMovieRepository { get; }

        public IDetailActorMovieRepository detailActorMovieRepository { get; }

        public IDetailGenresMovieRepository detailGenresMovieRepository { get; }

        public IDetailDirectorMovieRepository detailDirectorMovieRepository { get; }

        public IDetailUserMovieFavoriteRepository detailUserMovieFavoriteRepository { get; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
