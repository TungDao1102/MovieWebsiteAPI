using APIWebMovie.Interface;
using APIWebMovie.Models;

namespace APIWebMovie.Repository
{
    public class DetailUserMovieFavoriteRepository : GenericRepository<DetailUserMovieFavorite>, IDetailUserMovieFavoriteRepository
    {
        public DetailUserMovieFavoriteRepository(MovieWebContext context) : base(context)
        {
        }
    }
}
