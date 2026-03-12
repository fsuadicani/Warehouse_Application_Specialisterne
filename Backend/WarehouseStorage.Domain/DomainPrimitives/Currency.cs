using System;
using System.Text.RegularExpressions;

namespace WarehouseStorage.Domain.DomainPrimitives
{
    public class Currency
    {
        public readonly string value;

        public Currency(string currency)
        {
            Validate(currency);
            value = currency.ToUpperInvariant();
        }

        private void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Currency cannot be null or empty.");

            // ISO 4217: exactly 3 letters, no digits or symbols
            var pattern = @"^[A-Za-z]{3}$";

            if (!Regex.IsMatch(value, pattern))
                throw new ArgumentException("Currency must be exactly 3 letters (ISO 4217 format).");
        }
    }
}
