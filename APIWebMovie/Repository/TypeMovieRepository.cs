using APIWebMovie.Interface;
using APIWebMovie.Models;

using AutoMapper;
namespace APIWebMovie.Repository
{
    public class TypeMovieRepository : GenericRepository<TypeMovie>, ITypeMovieRepository
    {
        public TypeMovieRepository(MovieWebContext context, IMapper mapper) : base(context, mapper) { }
    }
}
