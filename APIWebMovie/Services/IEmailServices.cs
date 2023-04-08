using APIWebMovie.Services;

namespace APIWebMovie.Services
{
    public interface IEmailServices
    {
        void SendEmail(Message message);
    }
}
