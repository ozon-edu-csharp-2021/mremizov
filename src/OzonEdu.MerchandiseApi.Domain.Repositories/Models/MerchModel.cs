using System;
using OzonEdu.MerchandiseApi.Domain.Enumerations;

namespace OzonEdu.MerchandiseApi.Domain.Repositories.Models
{
    public sealed class MerchModel
    {
        public long Id { get; set; }

        public DateTime CreatedUtc { get; set; }

        public MerchModeEnum Mode { get; set; }

        public MerchStatusEnum Status { get; set; }
    }
}
