using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OzonEdu.MerchandiseApi.Domain.Common;

namespace OzonEdu.MerchandiseApi.Domain.ValueObjects
{
    public sealed class SkuList : ValueObject, IEnumerable<Sku>
    {
        private List<Sku> Items { get; }

        public SkuList(IEnumerable<Sku> items)
        {
            Items = new List<Sku>(items);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return Enumerable.Empty<object>();
        }

        public IEnumerator<Sku> GetEnumerator()
            => Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
