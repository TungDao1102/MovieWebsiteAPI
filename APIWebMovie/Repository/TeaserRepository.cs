using APIWebMovie.Interface;
using APIWebMovie.Models;

namespace APIWebMovie.Repository
{
    public class TeaserRepository : GenericRepository<Teaser>, ITeaserRepository
    {
        public TeaserRepository(MovieWebContext context) : base(context)
        {
        }
    }
}
