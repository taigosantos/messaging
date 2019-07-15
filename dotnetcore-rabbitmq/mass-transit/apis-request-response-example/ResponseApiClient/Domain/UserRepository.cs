using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResponseApiClient.Domain
{
    public class UserRepository
    {
        private readonly IList<User> _users;

        public UserRepository()
        {
            _users = new List<User>
            {
                new User(1, "Tiago", "Santos", "tiago@email.com"),
                new User(2, "Helene", "Santos", "helene@email.com"),
                new User(3, "Bruna", "Oliveira", "bruna@email.com")
            };
        }

        public User GetUserDetails(int userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId);
        }
    }
}
