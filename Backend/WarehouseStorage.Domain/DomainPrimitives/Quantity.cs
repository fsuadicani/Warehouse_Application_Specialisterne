using System;

namespace WarehouseStorage.Domain.DomainPrimitives
{
    public class Quantity
    {
        public readonly int value;

        public Quantity(int quantity)
        {
            Validate(quantity);
            value = quantity;
        }

        private void Validate(int input)
        {

            if (int.IsNegative(input))
                throw new ArgumentException("Quantity must be a positive number.");

            if (input > 5000)
                throw new ArgumentException("Quantity cannot exceed 5000.");
        }
    }
}
