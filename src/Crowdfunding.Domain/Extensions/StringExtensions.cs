using System.Linq;

namespace Crowdfunding.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string OnlyNumbers(this string text)
        {
            return string.Concat(text?.Where(char.IsDigit))?.Trim();
        }
    }
}
