using MyProfile.Common;
using MyProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyProfile.Specs
{
    public class GenderTypeSpec : Specification<User>
    {
        private readonly GenderType _gender;
        public GenderTypeSpec(GenderType gender)
        {
            _gender = gender;
        }


        public override Expression<Func<User, bool>> ToExpression()
        {
            return u => u.Gender == _gender;
        }
    }
}
