using System;

namespace OzonEdu.MerchandiseApi.Http.Contracts
{
    public sealed class MerchDto
    {
        public DateTime CreatedUtc { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }

        public long[] SkuList { get; set; }
    }
}
