using System;

namespace WarehouseStorage.Domain.DomainPrimitives
{
    public class Price
    {
        public readonly decimal value;

        public Price(decimal price)
        {
            Validate(price);
            value = price;
        }

        private void Validate(decimal input)
        {
            if (input <= 0)
                throw new ArgumentException("Price must be a positive number.");

            if (input > 100000)
                throw new ArgumentException("Price cannot exceed 100000.");

            // Check for max 2 decimal places
            var decimalPlaces = BitConverter.GetBytes(decimal.GetBits((decimal)input)[3])[2];
            if (decimalPlaces > 2)
                throw new ArgumentException("Price cannot have more than 2 decimal places.");
        }
    }
}
