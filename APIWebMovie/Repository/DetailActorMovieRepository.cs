using APIWebMovie.Interface;
using APIWebMovie.Models;

namespace APIWebMovie.Repository
{
    public class DetailActorMovieRepository : GenericRepository<DetailActorMovie>, IDetailActorMovieRepository
    {
        public DetailActorMovieRepository(MovieWebContext context) : base(context)
        {
        }
    }
}
