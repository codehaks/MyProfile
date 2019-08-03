using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProfile.Models
{
    public class UserCollection:IEnumerable
    {
        private List<User> _users = new List<User>();
        public UserCollection(List<User> users)
        {
            _users = users;
        }
        public User this[int id]
        {
            get => (User)_users.First(u=>u.Id==id);            
        }

        public User this[string name]
        {
            get => (User)_users.First(u => u.Name == name);
        }

        IEnumerator IEnumerable.GetEnumerator() => _users.GetEnumerator();
    }
}
