using System;
using OzonEdu.MerchandiseApi.Domain.Common;
using OzonEdu.MerchandiseApi.Domain.EmployeeAggregate;
using OzonEdu.MerchandiseApi.Domain.Enumerations;
using OzonEdu.MerchandiseApi.Domain.Exceptions;
using OzonEdu.MerchandiseApi.Domain.MerchPackAggregate;

namespace OzonEdu.MerchandiseApi.Domain.MerchAggregate
{
    public sealed class Merch : Entity
    {
        public Merch(
            MerchMode merchMode,
            Employee employee,
            MerchPack merchPack)
        {
            CreatedUtc = DateTime.UtcNow;
            Mode = merchMode;
            Status = MerchStatus.New;
            Employee = employee;
            MerchPack = merchPack;
        }

        public DateTime CreatedUtc { get; }

        public MerchMode Mode { get; private set; }

        public MerchStatus Status { get; private set; }

        public Employee Employee { get; }

        public MerchPack MerchPack { get; }

        public void Waiting()
        {
            if (Status == MerchStatus.Waiting)
            {
                return;
            }

            if (Status == MerchStatus.New)
            {
                Status = MerchStatus.Waiting;
            }

            throw new MerchInvalidStatusException();
        }

        public void Done()
        {
            if (Status == MerchStatus.Done)
            {
                return;
            }

            if (Status == MerchStatus.New || Status == MerchStatus.Waiting)
            {
                Status = MerchStatus.Done;
            }

            throw new MerchInvalidStatusException();
        }
    }
}
