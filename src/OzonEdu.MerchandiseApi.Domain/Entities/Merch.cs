using System;
using OzonEdu.MerchandiseApi.Domain.Common;
using OzonEdu.MerchandiseApi.Domain.Enumerations;
using OzonEdu.MerchandiseApi.Domain.Exceptions;

namespace OzonEdu.MerchandiseApi.Domain.Entities
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

        /// <summary>
        /// Для dapper.
        /// </summary>
        public Merch(
            long id,
            DateTime createdUtc,
            MerchMode mode,
            MerchStatus status,
            Employee employee,
            MerchPack merchPack) : base(id)
        {
            CreatedUtc = createdUtc;
            Mode = mode;
            Status = status;
            Employee = employee;
            MerchPack = merchPack;
        }

        /// <summary>
        /// Для тестов.
        /// </summary>
        public Merch(
            MerchMode merchMode,
            Employee employee,
            MerchPack merchPack,
            MerchStatus merchStatus)
            : this(merchMode, employee, merchPack)
        {
            Status = merchStatus;
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
                return;
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
                return;
            }

            throw new MerchInvalidStatusException();
        }
    }
}
