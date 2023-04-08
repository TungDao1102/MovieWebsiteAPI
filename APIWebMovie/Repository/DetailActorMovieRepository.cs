using APIWebMovie.Interface;
using APIWebMovie.Models;
using AutoMapper;

namespace APIWebMovie.Repository
{
    public class DetailActorMovieRepository : GenericRepository<DetailActorMovie>, IDetailActorMovieRepository
    {
        public DetailActorMovieRepository(MovieWebContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
