using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OzonEdu.MerchandiseApi.Domain.Services.Mocks
{
    public interface IStockApiClientMock
    {
        Task<bool> SkuListExist(IEnumerable<long> skuList, CancellationToken token);

        Task ReserveSkuList(IEnumerable<long> skuList, CancellationToken token);
    }
}
