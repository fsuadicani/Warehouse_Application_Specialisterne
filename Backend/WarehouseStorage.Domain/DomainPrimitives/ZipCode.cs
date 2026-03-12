using System;
using System.Text.RegularExpressions;

namespace WarehouseStorage.Domain.DomainPrimitives
{
    public class ZipCode
    {
        public readonly string value;

        public ZipCode(string code)
        {
            Validate(code);
            value = code;
        }

        private void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("ZIP cannot be null or empty.");

            if (value.Length < 4 || value.Length > 50)
                throw new ArgumentException("ZIP must be between 4 and 50 characters.");

            // Letters and digits only — no special characters
            var pattern = @"^[A-Za-z0-9]+$";

            if (!Regex.IsMatch(value, pattern))
                throw new ArgumentException("ZIP cannot contain special characters.");
        }
    }
}