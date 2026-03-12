using System;

namespace WarehouseStorage.Domain.DomainPrimitives
{
    public class Price
    {
        private readonly decimal value;

        public Price(string price)
        {
            Validate(price, out decimal parsed);
            value = parsed;
        }

        private void Validate(string input, out decimal parsedValue)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Price cannot be null or empty.");

            if (!decimal.TryParse(input, out parsedValue))
                throw new ArgumentException("Price must be a valid number.");

            if (parsedValue <= 0)
                throw new ArgumentException("Price must be a positive number.");

            if (parsedValue > 100000)
                throw new ArgumentException("Price cannot exceed 100000.");

            // Check for max 2 decimal places
            var decimalPlaces = BitConverter.GetBytes(decimal.GetBits((decimal)parsedValue)[3])[2];
            if (decimalPlaces > 2)
                throw new ArgumentException("Price cannot have more than 2 decimal places.");
        }

        public decimal ToDecimal() => value;

        public override string ToString() => value.ToString("F2");
    }
}
