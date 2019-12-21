using MyProfile.Common;
using MyProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProfile.Specs
{
    public class GenderTypeSpec : ISpecification<User>
    {
        private readonly GenderType _gender;
        public GenderTypeSpec(GenderType gender)
        {
            _gender = gender;
        }
        public bool IsSatisfiedBy(User Entity)
        {
            return Entity.Gender == _gender;
        }
    }
}
