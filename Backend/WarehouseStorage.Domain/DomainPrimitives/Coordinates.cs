using System;
using System.Text.RegularExpressions;

namespace WarehouseStorage.Domain.DomainPrimitives
{
    public class Coordinates
    {
        public readonly string value;

        public Coordinates(string coordinates)
        {
            Validate(coordinates);
            value = coordinates;
        }

        private void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Coordinates cannot be null or empty.");

            // Regex for formats like:
            // 40°45'11"N, 73°58'59"W
            // 34°03'08"S, 18°25'26"E
            // Allows optional seconds, but requires degrees + minutes at minimum
            var pattern =
                @"^\s*
                (\d{1,3})°\s*(\d{1,2})'\s*(\d{1,2})?""?\s*[NS]\s*,\s*
                (\d{1,3})°\s*(\d{1,2})'\s*(\d{1,2})?""?\s*[EW]
                \s*$";

            if (!Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace))
                throw new ArgumentException("Coordinates must follow standard geographic format (e.g., 40°45'11\"N, 73°58'59\"W).");
        }
    }
}