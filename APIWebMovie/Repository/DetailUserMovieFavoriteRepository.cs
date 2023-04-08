using APIWebMovie.Interface;
using APIWebMovie.Models;

using AutoMapper;
namespace APIWebMovie.Repository
{
    public class DetailUserMovieFavoriteRepository : GenericRepository<DetailUserMovieFavorite>, IDetailUserMovieFavoriteRepository
    {
        public DetailUserMovieFavoriteRepository(MovieWebContext context, IMapper mapper) : base(context, mapper) { }
    }
}
