using System.Collections.Generic;
using System.Linq;
using OzonEdu.MerchandiseApi.Domain.Entities;
using OzonEdu.MerchandiseApi.Domain.Enumerations;
using OzonEdu.MerchandiseApi.Domain.Exceptions;

namespace OzonEdu.MerchandiseApi.Domain.Aggregates
{
    public sealed class EmployeeWithMerchs
    {
        public Employee Employee { get; }

        public IReadOnlyCollection<Merch> Merchs => _merchs.AsReadOnly();
        private readonly List<Merch> _merchs;

        public EmployeeWithMerchs(Employee employee, IEnumerable<Merch> merchs)
        {
            Employee = employee;
            _merchs = merchs.ToList();
        }

        public Merch AddMerchToEmployee(MerchMode merchMode, MerchPack merchPack)
        {
            var merch = Merchs.FirstOrDefault(e => e.MerchPack == merchPack);

            if (merch == null)
            {
                merch = new Merch(merchMode, Employee, merchPack);
                _merchs.Add(merch);

                return merch;
            }

            // Если уже был запрос на выдачу мерча через REST API, но не хватило, то могло быть отправлено уведомление о пополении остатков.
            // Поэтому может быть произведен повторный запрос, и тогда в БД окажется в наличии мерч на статусе Waiting.
            // Этот повторный запрос можно и нужно обработать повторно и выдать мерч.
            if (merch.Status == MerchStatus.Waiting)
            {
                return merch;
            }

            throw new MerchAlreadyExistException();
        }
    }
}
