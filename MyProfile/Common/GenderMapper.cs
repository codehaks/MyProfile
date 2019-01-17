using AutoMapper;
using MyProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProfile.Common
{
    public class GenderMapper : ITypeConverter<GenderType, string>
    {
        public string Convert(GenderType source, string destination, ResolutionContext context)
        {
            switch (source)
            {
                case GenderType.None:
                    return "-";
                case GenderType.Male:
                    return "مرد";
                case GenderType.Female:
                    return "زن";

                default:
                    break;
            }

            return string.Empty;
        }
    }
}
