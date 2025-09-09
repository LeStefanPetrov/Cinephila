using System;

namespace Cinephila.Domain.Extensions
{
    public static class StringExtensions
    {
        public static DateTime? ToNullableDateTime(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            if (DateTime.TryParse(input, out DateTime parsedDate))
            {
                return parsedDate;
            }
            else
            {
                return null;
            }
        }
    }
}
