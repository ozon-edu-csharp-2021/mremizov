using System.Collections.Generic;
using OzonEdu.MerchandiseApi.Domain.Common;

namespace OzonEdu.MerchandiseApi.Domain.ValueObjects
{
    public sealed class MerchType : ValueObject
    {
        public MerchType(CSharpCourse.Core.Lib.Enums.MerchType value)
        {
            Value = value;
        }

        public CSharpCourse.Core.Lib.Enums.MerchType Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
