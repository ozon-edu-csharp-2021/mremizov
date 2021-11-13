using System.Collections.Generic;
using OzonEdu.MerchandiseApi.Domain.Common;

namespace OzonEdu.MerchandiseApi.Domain.ValueObjects
{
    public sealed class Sku : ValueObject
    {
        public long Value { get; }

        public Sku(long sku)
        {
            Value = sku;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
