using System.Collections.Generic;
using OzonEdu.MerchandiseApi.Domain.Common;

namespace OzonEdu.MerchandiseApi.Domain.ValueObjects
{
    public sealed class Name : ValueObject
    {
        public string Value { get; }

        public Name(string name)
        {
            Value = name;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
