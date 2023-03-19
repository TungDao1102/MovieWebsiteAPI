using APIWebMovie.Interface;
using APIWebMovie.Models;

namespace APIWebMovie.Repository
{
    public class TypeMovieRepository : GenericRepository<TypeMovie>, ITypeMovieRepository
    {
        public TypeMovieRepository(MovieWebContext context) : base(context)
        {
        }
    }
}
