using System.Threading;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Enums;
using OzonEdu.MerchandiseApi.Domain.Entities;

namespace OzonEdu.MerchandiseApi.Domain.Repositories
{
    public sealed class MerchPackRepository : IMerchPackRepository
    {
        public Task<MerchPack> FindBy(MerchType merchType, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}
