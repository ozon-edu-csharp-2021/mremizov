using System.Collections.Generic;
using System.Linq;
using OzonEdu.MerchandiseApi.Domain.Common;
using OzonEdu.MerchandiseApi.Domain.ValueObjects;

namespace OzonEdu.MerchandiseApi.Domain.Entities
{
    public sealed class MerchPack : Entity
    {
        public MerchPack(
            MerchType merchType,
            SkuList items)
        {
            MerchType = merchType;
            Items = items;
            SkuList = Items.Select(e => e.Value).ToArray();
        }

        public MerchType MerchType { get; }

        public SkuList Items { get; }

        public IReadOnlyCollection<long> SkuList { get; }
    }
}
