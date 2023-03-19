using APIWebMovie.Interface;
using APIWebMovie.Models;

namespace APIWebMovie.Repository
{
    public class ActorRepository : GenericRepository<Actor>, IActorRepository
    {
        public ActorRepository(MovieWebContext context) : base(context)
        {
        }
    }
}
