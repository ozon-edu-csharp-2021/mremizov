using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Enums;
using OzonEdu.MerchandiseApi.Domain.MerchAggregate;

namespace OzonEdu.MerchandiseApi.Domain.Repositories
{
    public interface IMerchRepository
    {
        Task<IEnumerable<Merch>> FindAll(long employeeId, CancellationToken token);

        Task<IEnumerable<Merch>> FindAll(IEnumerable<long> skuList, CancellationToken token);

        Task<Merch> Find(long employeeId, MerchType merchType, CancellationToken token);

        Task<bool> Exist(long employeeId, MerchType merchType, CancellationToken token);

        Task Save(Merch merch, CancellationToken token);
    }
}
