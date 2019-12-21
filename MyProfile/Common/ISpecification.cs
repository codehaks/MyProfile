using System.Collections.Generic;
using System.Text;

namespace MyProfile.Common
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T Entity);
    }
}
