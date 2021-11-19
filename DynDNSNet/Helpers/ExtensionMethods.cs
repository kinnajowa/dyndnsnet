using System.Collections.Generic;
using System.Linq;
using DynDNSNet.Entities;

namespace DynDNSNet.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static User WithoutPassword(this User user)
        {
            return new User {Username = user.Username, FirstName = user.FirstName, LastName = user.LastName};
        }
    }
}