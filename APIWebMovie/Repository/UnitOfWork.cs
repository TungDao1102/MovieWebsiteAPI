using APIWebMovie.Interface;
using APIWebMovie.Models;
using APIWebMovie.Services;
using AutoMapper;

namespace APIWebMovie.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieWebContext _context;
        private readonly IMapper _mapper;
        public UnitOfWork(MovieWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            reviewRepository = new ReviewRepository(context, _mapper);
            userRepository = new UserRepository(context, _mapper);
            actorRepository = new ActorRepository(context, _mapper);
            genresRepository = new GenresRepository(context, _mapper);
            movieRepository = new MovieRepository(context, _mapper);
            directorRepository = new DirectorRepository(context, _mapper);
            typeMovieRepository = new TypeMovieRepository(context, _mapper);
            billRepository = new BillRepository(context, _mapper);
            detailActorMovieRepository = new DetailActorMovieRepository(context, _mapper);
            detailGenresMovieRepository = new DetailGenresMovieRepository(context, _mapper);
            detailDirectorMovieRepository = new DetailDirectorMovieRepository(context, _mapper);
            detailUserMovieFavoriteRepository = new DetailUserMovieFavoriteRepository(context, _mapper);
        }

        public IReviewRepository reviewRepository { get; }

        public IUserRepository userRepository { get; }

        public IDirectorRepository directorRepository { get; }

        public IGenresRepository genresRepository { get; }

        public IMovieRepository movieRepository { get; }

        public IActorRepository actorRepository { get; }

        public ITypeMovieRepository typeMovieRepository { get; }
        public IBillRepository billRepository { get; }

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
