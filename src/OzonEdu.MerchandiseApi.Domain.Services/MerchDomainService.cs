using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Domain.Entities;
using OzonEdu.MerchandiseApi.Domain.Enumerations;
using OzonEdu.MerchandiseApi.Domain.Repositories;
using OzonEdu.MerchandiseApi.Domain.Services.Mocks;

namespace OzonEdu.MerchandiseApi.Domain.Services
{
    public sealed class MerchDomainService : IMerchDomainService
    {
        private readonly IMerchRepository _merchRepository;
        private readonly IEmployeeWithMerchsDomainService _employeeWithMerchsDomainService;
        private readonly IMerchPackDomainService _merchPackDomainService;
        private readonly IStockApiClientMock _stockApiClientMock;

        public MerchDomainService(
            IMerchRepository merchRepository,
            IEmployeeWithMerchsDomainService employeeWithMerchsDomainService,
            IMerchPackDomainService merchPackDomainService,
            IStockApiClientMock stockApiClientMock)
        {
            _merchRepository = merchRepository;

            _employeeWithMerchsDomainService = employeeWithMerchsDomainService;
            _merchPackDomainService = merchPackDomainService;

            _stockApiClientMock = stockApiClientMock;
        }

        public async Task<IEnumerable<Merch>> GetMerchInfo(EmployeeParameters employeeParameters, CancellationToken token)
        {
            var employee = await _employeeWithMerchsDomainService.FindOrCreateBy(employeeParameters, token);

            return employee.Merchs;
        }

        public async Task<Merch> GiveOutMerch(EmployeeParameters employeeParameters, MerchParameters merchParameters, CancellationToken token)
        {
            // TODO: нужна блокировка, чтобы нельзя было одновременно запустить в работу два одинаковых мерча.

            var employee = await _employeeWithMerchsDomainService.FindOrCreateBy(employeeParameters, token);
            var merchPack = await _merchPackDomainService.FindBy(merchParameters.MerchType, token);
            var merch = employee.AddMerchToEmployee(merchParameters.MerchMode, merchPack);

            return await GiveOutMerch(merch, token);
        }

        public async Task<IEnumerable<Merch>> GiveOutMerch(IEnumerable<long> shippedSkuList, CancellationToken token)
        {
            // TODO: нужна блокировка, чтобы нельзя было одновременно запустить в работу мерчи несколько раз.

            var employees = await _employeeWithMerchsDomainService.FindAllBy(shippedSkuList, token);

            foreach (var employee in employees)
            {
                foreach (var merch in employee.Merchs)
                {
                    if (merch.Mode == MerchMode.Manual)
                    {
                        await merch.Employee.SendCanPickupMerchNotification();
                    }
                    else if (merch.Mode == MerchMode.Auto)
                    {
                        await GiveOutMerch(merch, token);
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(nameof(merch.Mode));
                    }
                }
            }

            return employees
                .SelectMany(e => e.Merchs)
                .Where(e => e.Status == MerchStatus.Done)
                .ToArray();
        }

        private async Task<Merch> GiveOutMerch(Merch merch, CancellationToken token)
        {
            if (await _stockApiClientMock.TryReserve(merch.MerchPack.SkuList, token))
            {
                merch.Done();
                await _merchRepository.Save(merch, token);

                if (merch.Mode == MerchMode.Auto)
                {
                    await merch.Employee.SendPickupMerchNotification();
                }
            }
            else
            {
                merch.Waiting();
                await _merchRepository.Save(merch, token);

                // TODO: отправить e-mail HR, что мерч закончился и необходимо сделать поставку
                // TODO: где найти email HR?
            }

            return merch;
        }
    }
}
