using MyProfile.Common;
using MyProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProfile.Specs
{
    public class UserNameStartsWithSpec : ISpecification<User>
    {
        public UserNameStartsWithSpec(string startsWith)
        {
            StartsWith = startsWith;
        }
   
        public bool IsSatisfiedBy(User entity)
        {
            if (entity.Name.ToLower().StartsWith(StartsWith.ToLower()))
            {
                return true;
            }

            return false;
        }

        public string StartsWith { get;  }
    }
}
