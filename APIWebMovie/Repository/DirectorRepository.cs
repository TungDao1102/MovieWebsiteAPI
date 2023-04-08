using APIWebMovie.Interface;
using APIWebMovie.Models;

using AutoMapper;
namespace APIWebMovie.Repository
{
    public class DirectorRepository : GenericRepository<Director>, IDirectorRepository
    {
        public DirectorRepository(MovieWebContext context, IMapper mapper) : base(context, mapper) { }
    }
}
