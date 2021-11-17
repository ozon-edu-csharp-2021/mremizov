using System.Linq;
using OzonEdu.MerchandiseApi.Domain.Common;
using OzonEdu.MerchandiseApi.Domain.Exceptions;

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

        public static MerchStatus GetBy(MerchStatusEnum value)
        {
            var item = GetAll<MerchStatus>().FirstOrDefault(e => e.Id == (int)value);

            if (item == null)
            {
                throw new MerchStatusNotFoundException(value.ToString());
            }

            return item;
        }
    }

    public enum MerchStatusEnum
    {
        New = 1,
        Waiting,
        Done
    }
}
