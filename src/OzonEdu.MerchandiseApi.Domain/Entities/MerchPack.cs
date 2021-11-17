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
            SkuList = items;
            SkuListValues = SkuList.Select(e => e.Value).ToArray();
        }

        /// <summary>
        /// Для dapper.
        /// </summary>
        public MerchPack(
            long id,
            MerchType merchType,
            SkuList items) : base(id)
        {
            MerchType = merchType;
            SkuList = items;
            SkuListValues = SkuList.Select(e => e.Value).ToArray();
        }

        public MerchType MerchType { get; }

        public SkuList SkuList { get; }

        public IReadOnlyCollection<long> SkuListValues { get; }
    }
}
