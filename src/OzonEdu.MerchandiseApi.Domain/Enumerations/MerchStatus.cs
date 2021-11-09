using OzonEdu.MerchandiseApi.Domain.Common;

namespace OzonEdu.MerchandiseApi.Domain.Enumerations
{
    public sealed class MerchStatus : Enumeration
    {
        public static MerchStatus New = new((int)MerchStatusEnum.New, nameof(New));
        public static MerchStatus Waiting = new((int)MerchStatusEnum.Waiting, nameof(Waiting));
        public static MerchStatus Done = new((int)MerchStatusEnum.Done, nameof(Done));

        public MerchStatus(int id, string name) : base(id, name)
        {
        }
    }

    public enum MerchStatusEnum
    {
        New = 1,
        Waiting,
        Done
    }
}
