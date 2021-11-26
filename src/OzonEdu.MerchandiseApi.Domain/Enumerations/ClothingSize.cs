using System.Linq;
using OzonEdu.MerchandiseApi.Domain.Common;
using OzonEdu.MerchandiseApi.Domain.Exceptions;

namespace OzonEdu.MerchandiseApi.Domain.Enumerations
{
    public sealed class ClothingSize : Enumeration
    {
        public static ClothingSize XS = new((int)ClothingSizeEnum.XS, nameof(XS));
        public static ClothingSize S = new((int)ClothingSizeEnum.S, nameof(S));
        public static ClothingSize M = new((int)ClothingSizeEnum.M, nameof(M));
        public static ClothingSize L = new((int)ClothingSizeEnum.M, nameof(L));
        public static ClothingSize XL = new((int)ClothingSizeEnum.XL, nameof(XL));
        public static ClothingSize XXL = new((int)ClothingSizeEnum.XXL, nameof(XXL));

        public ClothingSize(int id, string name) : base(id, name)
        {
        }

        public static ClothingSize GetBy(string name)
        {
            var clothingSize = GetAll<ClothingSize>().FirstOrDefault(e => e.Name == name);

            if (clothingSize == null)
            {
                throw new ClothingSizeNotFoundException(name);
            }

            return clothingSize;
        }
    }

    public enum ClothingSizeEnum
    {
        XS = 1,
        S,
        M,
        L,
        XL,
        XXL
    }
}
