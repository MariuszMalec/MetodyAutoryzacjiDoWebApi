using WebAppiApiKey.Models;

namespace WebAppiApiKey.Extentions
{
    public static class ExtensionMethods
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static User WithoutPassword(this User user)
        {
            //user.Password = null;
            //return user;

            return new User()
            {
                Id = user.Id,
                Name = user.Name,
                Password = "*****"
            };

        }
    }
}
