using OzonEdu.MerchandiseApi.Domain.Common;
using OzonEdu.MerchandiseApi.Domain.ValueObjects;

namespace OzonEdu.MerchandiseApi.Domain.MerchPackAggregate
{
    public sealed class MerchPack : Entity
    {
        public MerchPack(
            MerchType merchType,
            SkuList items)
        {
            MerchType = merchType;
            Items = items;
        }

        public MerchType MerchType { get; }

        public SkuList Items { get; }
    }
}
