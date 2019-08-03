using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProfile.Models
{
    public class UserDictionary :IEnumerable
    {
        private Dictionary<int,User> _users = new Dictionary<int, User>();

        public UserDictionary(List<User> users)
        {
            foreach (var item in users)
            {
                _users.Add(item.Id, item);
            }
        }

        public User this[int id]
        {
            get => (User)_users[id];
            set => _users.Add(id, value);
        }

        

        public void Add(User user)
        {
            if (user.Age>20)
            {
                _users.Add(user.Id, user);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => _users.GetEnumerator();

    }
}
