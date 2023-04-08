using APIWebMovie.Interface;
using APIWebMovie.Models;
using AutoMapper;

namespace APIWebMovie.Repository
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieWebContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
