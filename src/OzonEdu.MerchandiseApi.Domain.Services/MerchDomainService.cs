using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Enums;
using CSharpCourse.Core.Lib.Events;
using OzonEdu.MerchandiseApi.Domain.MerchAggregate;

namespace OzonEdu.MerchandiseApi.Domain.Services
{
    public sealed class MerchDomainService : IMerchDomainService
    {
        public Task<IEnumerable<Merch>> GetMerchInfo(long employeeId, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task<Merch> GiveOutMerch(long employeeId, MerchType merchType, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task<Merch> GiveOutMerch(NotificationEvent notificationEvent, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task<Merch> GiveOutMerch(SupplyShippedEvent supplyShippedEvent, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}
