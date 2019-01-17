using AutoMapper;
using System;

namespace MyProfile.Common
{
    public class StringTrimmer : ITypeConverter<string, string>
    {
        public string Convert(string source, string destination, ResolutionContext context)
        {
            if (source != null)
            {
                if (!(String.IsNullOrEmpty(source.ToString())))
                {
                    return source.ToString().Trim();
                }
            }
            return string.Empty;
        }
    }
}