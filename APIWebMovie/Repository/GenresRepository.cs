using APIWebMovie.Interface;
using APIWebMovie.Models;

using AutoMapper;
namespace APIWebMovie.Repository
{
    public class GenresRepository : GenericRepository<Genre>, IGenresRepository
    {
        public GenresRepository(MovieWebContext context, IMapper mapper) : base(context, mapper) { }
    }
}
