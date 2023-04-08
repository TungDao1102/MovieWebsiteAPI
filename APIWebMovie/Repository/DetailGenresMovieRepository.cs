using APIWebMovie.Interface;
using APIWebMovie.Models;
using AutoMapper;

namespace APIWebMovie.Repository
{
    public class DetailGenresMovieRepository : GenericRepository<DetailGenresMovie>, IDetailGenresMovieRepository
    {
        public DetailGenresMovieRepository(MovieWebContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
