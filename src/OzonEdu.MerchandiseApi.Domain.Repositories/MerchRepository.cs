using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Domain.Entities;

namespace OzonEdu.MerchandiseApi.Domain.Repositories
{
    public sealed class MerchRepository : IMerchRepository
    {
        public Task<IEnumerable<Merch>> FindAllBy(long employeeId, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Merch>> FindAllBy(IEnumerable<long> skuList, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task Save(Merch merch, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}
