using OzonEdu.MerchandiseApi.Domain.Common;
using OzonEdu.MerchandiseApi.Domain.EmployeeAggregate;
using OzonEdu.MerchandiseApi.Domain.Enumerations;
using OzonEdu.MerchandiseApi.Domain.MerchPackAggregate;

namespace OzonEdu.MerchandiseApi.Domain.MerchAggregate
{
    public sealed class Merch : Entity
    {
        public Merch(
            Employee employee,
            MerchPack merchPack)
        {
            Status = MerchStatus.New;
            Employee = employee;
            MerchPack = merchPack;
        }

        public MerchStatus Status { get; }

        public Employee Employee { get; }

        public MerchPack MerchPack { get; }
    }
}
