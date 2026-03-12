using System;
using System.Text.RegularExpressions;

namespace WarehouseStorage.Domain.DomainPrimitives
{
    public class City
    {
        public readonly string value;

        public City(string city)
        {
            Validate(city);
            value = city;
        }

        private void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("City name cannot be null or empty.");

            if (value.Length > 50)
                throw new ArgumentException("City name cannot exceed 50 characters.");

            // Allow letters, spaces, hyphens, apostrophes, and periods
            // Common in real-world city names: St. Louis, O'Fallon, Rio de Janeiro, Ho Chi Minh City
            var pattern = @"^[a-zA-Z\s\.\-']+$";

            if (!Regex.IsMatch(value, pattern))
                throw new ArgumentException("City name contains invalid characters.");
        }
    }
}