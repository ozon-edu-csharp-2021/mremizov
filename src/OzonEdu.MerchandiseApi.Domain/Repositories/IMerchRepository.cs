using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Domain.Common;
using OzonEdu.MerchandiseApi.Domain.Entities;

namespace OzonEdu.MerchandiseApi.Domain.Repositories
{
    public interface IMerchRepository : IRepository
    {
        Task<IEnumerable<Merch>> FindAllBy(Employee employee, CancellationToken token);

        Task<IEnumerable<Merch>> FindAllBy(IEnumerable<long> skuList, CancellationToken token);

        Task Save(Merch merch, CancellationToken token);
    }
}
