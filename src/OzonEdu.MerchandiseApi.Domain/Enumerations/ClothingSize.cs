using System.Linq;
using OzonEdu.MerchandiseApi.Domain.Common;
using OzonEdu.MerchandiseApi.Domain.Exceptions;

namespace OzonEdu.MerchandiseApi.Domain.Enumerations
{
    public sealed class ClothingSize : Enumeration
    {
        public static ClothingSize XS = new(1, nameof(XS));
        public static ClothingSize S = new(2, nameof(S));
        public static ClothingSize M = new(3, nameof(M));
        public static ClothingSize L = new(4, nameof(L));
        public static ClothingSize XL = new(5, nameof(XL));
        public static ClothingSize XXL = new(6, nameof(XXL));

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
}
