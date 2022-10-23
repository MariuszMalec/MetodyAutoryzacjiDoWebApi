using WebAppiBasic.Models;

namespace WebAppiBasic.Data
{
    public class UsersStorage
    {
        public static List<User> Users = new List<User>
        {
            new User {Id = 1, Name = "Mariusz", Password = "mariusz" },
            new User {Id = 2, Name = "Dariusz", Password = "dariusz" }
        };
    }
}
