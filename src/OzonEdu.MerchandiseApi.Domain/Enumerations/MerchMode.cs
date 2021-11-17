using OzonEdu.MerchandiseApi.Domain.Common;

namespace OzonEdu.MerchandiseApi.Domain.Enumerations
{
    public sealed class MerchMode : Enumeration
    {
        public static MerchMode Manual = new((int)MerchModeEnum.Manual, nameof(Manual));
        public static MerchMode Auto = new((int)MerchModeEnum.Auto, nameof(Auto));

        public MerchMode(int id, string name) : base(id, name)
        {
        }
    }

    public enum MerchModeEnum
    {
        Manual = 1,
        Auto
    }
}
