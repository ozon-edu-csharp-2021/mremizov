using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Events;
using OzonEdu.MerchandiseApi.Common;
using OzonEdu.MerchandiseApi.Domain.Enumerations;
using OzonEdu.MerchandiseApi.Domain.Exceptions;
using OzonEdu.MerchandiseApi.Domain.MerchAggregate;
using OzonEdu.MerchandiseApi.Domain.Repositories;
using OzonEdu.MerchandiseApi.Domain.Services.Mocks;

namespace OzonEdu.MerchandiseApi.Domain.Services
{
    public sealed class MerchDomainService : IMerchDomainService
    {
        private readonly IMerchRepository _merchRepository;
        private readonly IMerchPackRepository _merchPackRepository;
        private readonly IEmployeeApiClientMock _employeeApiClientMock;
        private readonly IStockApiClientMock _stockApiClientMock;

        public MerchDomainService(
            IMerchRepository merchRepository,
            IMerchPackRepository merchPackRepository,
            IEmployeeApiClientMock employeeApiClientMock,
            IStockApiClientMock stockApiClientMock)
        {
            _merchRepository = merchRepository;
            _merchPackRepository = merchPackRepository;
            _employeeApiClientMock = employeeApiClientMock;
            _stockApiClientMock = stockApiClientMock;
        }

        public async Task<IEnumerable<Merch>> GetMerchInfo(GetMerchInfoParameters parameters, CancellationToken token)
        {
            var employee = await _employeeApiClientMock.GetBy(
                parameters.EmployeeEmail,
                parameters.EmployeeName,
                token);

            if (employee != null)
            {
                throw new EmployeeNotFoundException();
            }

            return await _merchRepository.FindAll(employee.Id, token);
        }

        public async Task<Merch> GiveOutMerch(GiveOutMerchParameters parameters, CancellationToken token)
        {
            var employee = await _employeeApiClientMock.GetBy(
                parameters.EmployeeEmail,
                parameters.EmployeeName,
                token);

            if (employee != null)
            {
                throw new EmployeeNotFoundException();
            }

            var merchPack = await _merchPackRepository.GetBy(parameters.MerchType, token);
            if (merchPack != null)
            {
                throw new MerchPackNotFoundException();
            }

            // TODO: методы не оптимальны, потому что эта проверка вызывается в самом конце,
            // лучше вызывать ее в самом начале, чтобы сократить обращение к БД и внешним микросервисам.
            var merch = await _merchRepository.Find(employee.Id, merchPack.MerchType.Value, token);

            // Если уже был запрос на выдачу мерча через REST API, но не хватило, то могло быть отправлено уведомление о пополении остатков.
            // Поэтому может быть произведен повторный запрос, и тогда в БД окажется в наличии мерч на статусе Waiting.
            // Этот повторный запрос можно и нужно обработать повторно и выдать мерч.
            if (merch != null && merch.Status != MerchStatus.Waiting)
            {
                throw new MerchAlreadyExistException();
            }

            if (merch == null)
            {
                merch = new Merch(MerchMode.Manual, employee, merchPack);
            }

            var merchPackSkuList = merch.MerchPack.Items.Select(e => e.Value).ToArray();
            var merchPackExist = await _stockApiClientMock.SkuListExist(merchPackSkuList, token);

            if (merchPackExist)
            {
                await _stockApiClientMock.ReserveSkuList(merchPackSkuList, token);

                merch.Done();
                await _merchRepository.Save(merch, token);
            }
            else
            {
                merch.Waiting();
                await _merchRepository.Save(merch, token);

                // TODO: отправить уведомление HR, что мерч закончился и необходимо сделать поставку
            }

            return merch;
        }

        public async Task<Merch> GiveOutMerch(NotificationEvent notificationEvent, CancellationToken token)
        {
            var employee = await _employeeApiClientMock.GetBy(
                notificationEvent.EmployeeEmail,
                notificationEvent.EmployeeName,
                token);

            if (employee != null)
            {
                throw new EmployeeNotFoundException();
            }

            var merchType = notificationEvent.Payload.To<MerchDeliveryEventPayload>().MerchType;
            var merchPack = await _merchPackRepository.GetBy(merchType, token);
            if (merchPack != null)
            {
                throw new MerchPackNotFoundException();
            }

            // TODO: методы не оптимальны, потому что эта проверка вызывается в самом конце,
            // лучше вызывать ее в самом начале, чтобы сократить обращение к БД и внешним микросервисам.
            var merchExist = await _merchRepository.Exist(employee.Id, merchPack.MerchType.Value, token);
            if (merchExist)
            {
                throw new MerchAlreadyExistException();
            }

            var merch = new Merch(MerchMode.Auto, employee, merchPack);

            var merchPackSkuList = merch.MerchPack.Items.Select(e => e.Value).ToArray();
            var merchPackExist = await _stockApiClientMock.SkuListExist(merchPackSkuList, token);

            if (merchPackExist)
            {
                await _stockApiClientMock.ReserveSkuList(merchPackSkuList, token);

                merch.Done();
                await _merchRepository.Save(merch, token);

                // TODO: Отправить e-mail сотруднику, что ему необходимо подойти к HR для получения мерча
            }
            else
            {
                merch.Waiting();
                await _merchRepository.Save(merch, token);

                // TODO: отправить уведомление HR, что мерч закончился и необходимо сделать поставку
            }

            return merch;
        }

        public async Task<IEnumerable<Merch>> GiveOutMerch(SupplyShippedEvent supplyShippedEvent, CancellationToken token)
        {
            var shippedSkuList = supplyShippedEvent.Items.Select(e => e.SkuId).ToArray();
            var merchs = await _merchRepository.FindAll(shippedSkuList, token);

            foreach (var merch in merchs)
            {
                if (merch.Mode == MerchMode.Manual)
                {
                    // TODO: отправить уведомление, что интересующий его мерч появился на остатках
                    // TODO: ну допустим, он узнал, и снова вызвал метод api/merch/give-out-merch - у нас нет продолжения выдачи мерча
                }
                else if (merch.Mode == MerchMode.Auto)
                {
                    var merchPackSkuList = merch.MerchPack.Items.Select(e => e.Value).ToArray();
                    var merchPackExist = await _stockApiClientMock.SkuListExist(merchPackSkuList, token);

                    if (merchPackExist)
                    {
                        await _stockApiClientMock.ReserveSkuList(merchPackSkuList, token);
                        merch.Done();

                        await _merchRepository.Save(merch, token);
                        // TODO: Отправить e-mail сотруднику, что ему необходимо подойти к HR для получения мерча
                    }
                }
                else
                {
                    throw new System.ArgumentOutOfRangeException(nameof(merch.Mode));
                }
            }

            return merchs.Where(e => e.Status == MerchStatus.Done);
        }
    }
}
