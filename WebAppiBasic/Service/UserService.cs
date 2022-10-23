using WebAppiBasic.Data;
using WebAppiBasic.Extentions;
using WebAppiBasic.Models;

namespace WebAppiBasic.Service
{
    public class UserService : IUserService
    {
        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => UsersStorage.Users.SingleOrDefault(x => x.Name == username && x.Password == password));
            if (user == null)
                return null;
            return user;//.WithoutPassword();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.Run(() => UsersStorage.Users.WithoutPasswords());
        }

        public User GetById(int id)
        {
            return UsersStorage.Users.WithoutPasswords().SingleOrDefault(p => p.Id == id);

        }
    }
}
