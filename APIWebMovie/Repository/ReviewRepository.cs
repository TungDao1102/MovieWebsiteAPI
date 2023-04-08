using APIWebMovie.Interface;
using APIWebMovie.Models;
using AutoMapper;

namespace APIWebMovie.Repository
{
    public class ReviewRepository : GenericRepository<ReView>, IReviewRepository
    {
        public ReviewRepository(MovieWebContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
