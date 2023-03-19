using APIWebMovie.Interface;
using APIWebMovie.Models;


namespace APIWebMovie.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(MovieWebContext webPhimContext) : base(webPhimContext) { }

    }
}
