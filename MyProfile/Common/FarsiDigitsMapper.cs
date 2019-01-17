using AutoMapper;

namespace MyProfile.Common
{
    public class FarsiDigitsMapper : ITypeConverter<int, string>
    {
        public string Convert(int source, string destination, ResolutionContext context)
        {
            return source.ToString()
                .Replace("0", "۰")
                .Replace("1", "۱")
                .Replace("2", "۲")
                .Replace("3", "۳")
                .Replace("4", "۴")
                .Replace("5", "۵")
                .Replace("6", "۶")
                .Replace("7", "v")
                .Replace("8", "۸")
                .Replace("9", "۹");
        }
    }
}
