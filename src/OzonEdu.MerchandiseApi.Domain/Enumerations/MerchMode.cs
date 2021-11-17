using System.Linq;
using OzonEdu.MerchandiseApi.Domain.Common;
using OzonEdu.MerchandiseApi.Domain.Exceptions;

namespace OzonEdu.MerchandiseApi.Domain.Enumerations
{
    public sealed class MerchMode : Enumeration
    {
        public static MerchMode Manual = new((int)MerchModeEnum.Manual, nameof(Manual));
        public static MerchMode Auto = new((int)MerchModeEnum.Auto, nameof(Auto));

        public MerchMode(int id, string name) : base(id, name)
        {
        }

        public static MerchMode GetBy(MerchModeEnum value)
        {
            var item = GetAll<MerchMode>().FirstOrDefault(e => e.Id == (int)value);

            if (item == null)
            {
                throw new MerchModeNotFoundException(value.ToString());
            }

            return item;
        }
    }

    public enum MerchModeEnum
    {
        Manual = 1,
        Auto
    }
}
