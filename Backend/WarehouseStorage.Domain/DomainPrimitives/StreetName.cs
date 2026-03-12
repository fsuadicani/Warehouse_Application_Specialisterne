using System;

namespace WarehouseStorage.Domain.DomainPrimitives
{
    public class StreetName : Name
    {
        public StreetName(string name) : base(name)
        {
        }

        // No extra rules for now — but override is ready if needed later
        protected override void Validate(string value)
        {
            base.Validate(value);
        }
    }
}
