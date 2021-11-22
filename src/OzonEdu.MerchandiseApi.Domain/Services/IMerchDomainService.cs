using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Enums;
using OzonEdu.MerchandiseApi.Domain.Common;
using OzonEdu.MerchandiseApi.Domain.Entities;
using OzonEdu.MerchandiseApi.Domain.Enumerations;

namespace OzonEdu.MerchandiseApi.Domain.Services
{
    public interface IMerchDomainService : IDomainService
    {
        Task<Merch> GiveOutMerch(EmployeeParameters employeeParameters, MerchParameters merchParameters, CancellationToken token);

        Task<IEnumerable<Merch>> GiveOutMerch(IEnumerable<long> shippedSkuList, CancellationToken token);
    }

    // TODO: что будет на входе через rest api? employeeId или employeeEmail+employeeName?
    // TODO: куда сложить класс? было бы хорошо сложить его в CSharpCourse.Core.Lib.
    // TODO: нужен ли он? можно ли как-то использовать GetMerchInfoRequest из http контрактов?
    public sealed class EmployeeParameters
    {
        public string EmployeeEmail { get; set; }

        public string EmployeeName { get; set; }
    }

    // TODO: что будет на входе через rest api? employeeId или employeeEmail+employeeName?
    // TODO: куда сложить класс? было бы хорошо сложить его в CSharpCourse.Core.Lib.
    // TODO: нужен ли он? можно ли как-то использовать GiveOutMerchRequest из http контрактов?

    public sealed class MerchParameters
    {
        public MerchType MerchType { get; set; }

        public MerchMode MerchMode { get; set; }
    }
}
