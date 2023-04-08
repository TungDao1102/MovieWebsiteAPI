using APIWebMovie.Interface;
using APIWebMovie.Models;
using AutoMapper;

namespace APIWebMovie.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(MovieWebContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
