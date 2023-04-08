using APIWebMovie.Interface;
using APIWebMovie.Models;
using AutoMapper;

namespace APIWebMovie.Repository
{
    public class DetailDirectorMovieRepository : GenericRepository<DetailDirectorMovie>, IDetailDirectorMovieRepository
    {
        public DetailDirectorMovieRepository(MovieWebContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
