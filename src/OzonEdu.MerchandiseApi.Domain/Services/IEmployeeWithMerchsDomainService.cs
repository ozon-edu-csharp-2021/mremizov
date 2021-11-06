using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Domain.Aggregates;

namespace OzonEdu.MerchandiseApi.Domain.Services
{
    public interface IEmployeeWithMerchsDomainService
    {
        Task<EmployeeWithMerchs> FindOrCreateBy(EmployeeParameters parameters, CancellationToken token);

        Task<IEnumerable<EmployeeWithMerchs>> FindAllBy(IEnumerable<long> skuList, CancellationToken token);
    }
}
