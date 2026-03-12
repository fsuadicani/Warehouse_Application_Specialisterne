using System;
using System.Text.RegularExpressions;

namespace WarehouseStorage.Domain.DomainPrimitives
{
    public class PickUpCode
    {
        public readonly string value;

        public PickUpCode(string code)
        {
            Validate(code);
            value = code;
        }

        private void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Pick-up code cannot be null or empty.");

            if (value.Length < 10 || value.Length > 50)
                throw new ArgumentException("Pick-up code must be between 10 and 50 characters.");

            // Letters and digits only — no special characters
            var pattern = @"^[A-Za-z0-9]+$";

            if (!Regex.IsMatch(value, pattern))
                throw new ArgumentException("Pick-up code cannot contain special characters.");
        }
    }
}