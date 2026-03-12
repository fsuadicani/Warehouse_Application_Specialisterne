using System;
using System.Text.RegularExpressions;

namespace WarehouseStorage.Domain.DomainPrimitives
{
    public class StockLocation
    {
        public readonly string value;

        public StockLocation(string location)
        {
            Validate(location);
            value = location;
        }

        private void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Location cannot be null or empty.");

            if (value.Length < 10 || value.Length > 50)
                throw new ArgumentException("Location must be between 10 and 50 characters.");

            // Letters and digits only — no special characters
            var pattern = @"^[A-Za-z0-9]+$";

            if (!Regex.IsMatch(value, pattern))
                throw new ArgumentException("Location cannot contain special characters.");
        }
    }
}