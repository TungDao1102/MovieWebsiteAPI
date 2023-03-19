using APIWebMovie.Interface;
using APIWebMovie.Models;

namespace APIWebMovie.Repository
{
    public class ReviewRepository : GenericRepository<ReView>, IReviewRepository
    {
        public ReviewRepository(MovieWebContext webPhimContext) : base(webPhimContext)
        {

        }
    }
}
