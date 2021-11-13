using System.Threading;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Enums;
using OzonEdu.MerchandiseApi.Domain.Entities;
using OzonEdu.MerchandiseApi.Domain.Enumerations;

namespace OzonEdu.MerchandiseApi.Domain.Repositories
{
    public sealed class MerchPackRepository : IMerchPackRepository
    {
        public Task<MerchPack> FindBy(MerchType merchType, ClothingSize clothingSize, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}
