using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OzonEdu.MerchandiseApi.Domain.Services.Mocks
{
    public interface IStockApiClientMock
    {
        Task<bool> TryReserve(IEnumerable<long> skuList, CancellationToken token);
    }

    public sealed class StockApiClientMock : IStockApiClientMock
    {
        public Task<bool> TryReserve(IEnumerable<long> skuList, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}
