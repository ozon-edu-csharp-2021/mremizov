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
        Task<IEnumerable<Merch>> GetMerchInfo(GetMerchInfoParameters parameters, CancellationToken token);

        Task<Merch> GiveOutMerch(GiveOutMerchParameters parameters, CancellationToken token);

        Task<Merch> GiveOutMerch(NotificationEvent notificationEvent, CancellationToken token);

        Task<IEnumerable<Merch>> GiveOutMerch(SupplyShippedEvent supplyShippedEvent, CancellationToken token);
    }

    // TODO: что будет на входе через rest api? employeeId или employeeEmail+employeeName?
    // TODO: куда сложить класс? было бы хорошо сложить его в CSharpCourse.Core.Lib. а нужен ли он?
    // TODO: нужен ли он? можно ли как-то использовать GetMerchInfoRequest из http контрактов?
    public sealed class GetMerchInfoParameters
    {
        public string EmployeeEmail { get; set; }

        public string EmployeeName { get; set; }
    }

    // TODO: что будет на входе через rest api? employeeId или employeeEmail+employeeName?
    // TODO: куда сложить класс? было бы хорошо сложить его в CSharpCourse.Core.Lib.
    // TODO: нужен ли он? можно ли как-то использовать GiveOutMerchRequest из http контрактов?

    public sealed class GiveOutMerchParameters
    {
        public string EmployeeEmail { get; set; }

        public string EmployeeName { get; set; }

        public MerchType MerchType { get; set; }
    }
}
