using System.Threading;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Enums;
using OzonEdu.MerchandiseApi.Domain.MerchPackAggregate;

namespace OzonEdu.MerchandiseApi.Domain.Repositories
{
    public interface IMerchPackRepository
    {
        Task<MerchPack> GetBy(MerchType merchType, CancellationToken token);
    }
}
