using APIWebMovie.Interface;
using APIWebMovie.Models;

namespace APIWebMovie.Repository
{
    public class DetailDirectorMovieRepository : GenericRepository<DetailDirectorMovie>, IDetailDirectorMovieRepository
    {
        public DetailDirectorMovieRepository(MovieWebContext context) : base(context)
        {
        }
    }
}
