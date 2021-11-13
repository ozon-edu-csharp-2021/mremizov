using System.Collections.Generic;
using OzonEdu.MerchandiseApi.Domain.Common;

namespace OzonEdu.MerchandiseApi.Domain.ValueObjects
{
    public sealed class Quantity : ValueObject
    {
        public Quantity(int value)
        {
            Value = value;
        }

        public int Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
