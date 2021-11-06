using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Domain.Aggregates;
using OzonEdu.MerchandiseApi.Domain.Common;

namespace OzonEdu.MerchandiseApi.Domain.Services
{
    public interface IEmployeeWithMerchsDomainService : IDomainService
    {
        Task<EmployeeWithMerchs> FindOrCreateBy(EmployeeParameters parameters, CancellationToken token);

        Task<IEnumerable<EmployeeWithMerchs>> FindAllBy(IEnumerable<long> skuList, CancellationToken token);
    }
}
