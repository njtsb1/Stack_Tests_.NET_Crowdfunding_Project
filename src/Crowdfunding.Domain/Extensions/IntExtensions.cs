using System.Globalization;

namespace Crowdfunding.Domain.Extensions
{
    public static class IntExtensions
    {
        public static string ToMoneyBrString(this int valor)
        {
            return value.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
        }
    }
}
