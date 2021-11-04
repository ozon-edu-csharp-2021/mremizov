using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Enums;
using CSharpCourse.Core.Lib.Events;
using OzonEdu.MerchandiseApi.Domain.MerchAggregate;

namespace OzonEdu.MerchandiseApi.Domain.Services
{
    public interface IMerchDomainService
    {
        Task<IEnumerable<Merch>> GetMerchInfo(long employeeId, CancellationToken token);

        Task<Merch> GiveOutMerch(long employeeId, MerchType merchType, CancellationToken token);

        Task<Merch> GiveOutMerch(NotificationEvent notificationEvent, CancellationToken token);

        Task<Merch> GiveOutMerch(SupplyShippedEvent supplyShippedEvent, CancellationToken token);
    }
}
