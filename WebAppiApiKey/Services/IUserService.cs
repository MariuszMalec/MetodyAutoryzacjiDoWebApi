using WebAppiApiKey.Models;

namespace WebAppiApiKey.Service
{
    public interface IUserService
    {
        User GetById(int id);
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
    }
}
