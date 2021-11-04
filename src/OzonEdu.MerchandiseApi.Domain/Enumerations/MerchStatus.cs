using OzonEdu.MerchandiseApi.Domain.Common;

namespace OzonEdu.MerchandiseApi.Domain.Enumerations
{
    public sealed class MerchStatus : Enumeration
    {
        public static MerchStatus New = new(1, nameof(New));
        public static MerchStatus Waiting = new(2, nameof(Waiting));
        public static MerchStatus Done = new(3, nameof(Done));

        public MerchStatus(int id, string name) : base(id, name)
        {
        }
    }
}
