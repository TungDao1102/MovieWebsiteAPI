using APIWebMovie.Interface;
using APIWebMovie.Models;

namespace APIWebMovie.Repository
{
    public class GenresRepository : GenericRepository<Genre>, IGenresRepository
    {
        public GenresRepository(MovieWebContext context) : base(context)
        {
        }
    }
}
