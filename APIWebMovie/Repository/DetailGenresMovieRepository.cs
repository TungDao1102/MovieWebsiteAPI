using APIWebMovie.Interface;
using APIWebMovie.Models;

namespace APIWebMovie.Repository
{
    public class DetailGenresMovieRepository : GenericRepository<DetailGenresMovie>, IDetailGenresMovieRepository
    {
        public DetailGenresMovieRepository(MovieWebContext context) : base(context)
        {
        }
    }
}
