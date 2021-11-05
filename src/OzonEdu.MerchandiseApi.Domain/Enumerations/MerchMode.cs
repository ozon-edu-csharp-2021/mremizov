using OzonEdu.MerchandiseApi.Domain.Common;

namespace OzonEdu.MerchandiseApi.Domain.Enumerations
{
    public sealed class MerchMode : Enumeration
    {
        public static MerchMode Manual = new(1, nameof(Manual));
        public static MerchMode Auto = new(2, nameof(Auto));

        public MerchMode(int id, string name) : base(id, name)
        {
        }
    }
}
