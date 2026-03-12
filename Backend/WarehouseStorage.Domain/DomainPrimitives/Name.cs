using System;
using System.Text.RegularExpressions;

namespace WarehouseStorage.Domain.DomainPrimitives
{
    public class Name
    {
        public readonly string value;

        public Name(string name)
        {
            Validate(name);
            value = name;
        }

        protected virtual void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be null or empty.");

            if (value.Length > 50)
                throw new ArgumentException("Name cannot exceed 50 characters.");

            // Allow letters, spaces, hyphens, apostrophes, and periods
            // Covers names like: Anne-Marie, O'Connor, St. John, Jean Luc
            var pattern = @"^[a-zA-Z\s\.\-']+$";

            if (!Regex.IsMatch(value, pattern))
                throw new ArgumentException("Name contains invalid characters.");
        }
    }
}