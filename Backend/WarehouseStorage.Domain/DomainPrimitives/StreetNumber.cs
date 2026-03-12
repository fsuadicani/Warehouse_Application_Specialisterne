using System;
using System.Text.RegularExpressions;

namespace WarehouseStorage.Domain.DomainPrimitives
{
    public class StreetNumber
    {
        public readonly string value;

        public StreetNumber(string number)
        {
            Validate(number);
            value = number;
        }

        private void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Street number cannot be null or empty.");

            if (value.Length < 1 || value.Length > 50)
                throw new ArgumentException("Street number must be between 1 and 50 characters.");

            // Letters and digits only — no special characters
            var pattern = @"^[A-Za-z0-9]+$";

            if (!Regex.IsMatch(value, pattern))
                throw new ArgumentException("Street number cannot contain special characters.");
        }
    }
}