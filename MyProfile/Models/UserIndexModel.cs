using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProfile.Models
{
    public class UserIndexModel
    {
        public IEnumerable<User> Users { get; set; }
        public GenderType GenderType { get; set; }
    }
}
