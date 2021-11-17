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
        private readonly IEmailServiceMock _emailServiceMock;
        private readonly IStockApiClientMock _stockApiClientMock;

        public MerchDomainService(
            IMerchRepository merchRepository,
            IEmployeeWithMerchsDomainService employeeWithMerchsDomainService,
            IMerchPackDomainService merchPackDomainService,
            IEmailServiceMock emailServiceMock,
            IStockApiClientMock stockApiClientMock)
        {
            _merchRepository = merchRepository;
            _employeeWithMerchsDomainService = employeeWithMerchsDomainService;
            _merchPackDomainService = merchPackDomainService;
            _emailServiceMock = emailServiceMock;
            _stockApiClientMock = stockApiClientMock;
        }

        public async Task<Merch> GiveOutMerch(EmployeeParameters employeeParameters, MerchParameters merchParameters, CancellationToken token)
        {
            // TODO: нужна блокировка, чтобы нельзя было одновременно запустить в работу два одинаковых мерча.

            var employee = await _employeeWithMerchsDomainService.FindBy(employeeParameters, token);
            var merchPack = await _merchPackDomainService.FindBy(merchParameters.MerchType, employee.Employee.ClothingSize, token);
            var merch = employee.AddMerchToEmployee(merchParameters.MerchMode, merchPack);

            return await GiveOutMerch(merch, token);
        }

        public async Task<IEnumerable<Merch>> GiveOutMerch(IEnumerable<long> shippedSkuList, CancellationToken token)
        {
            // TODO: нужна блокировка, чтобы нельзя было одновременно запустить в работу мерчи несколько раз.

            // TODO: Employee в базе не храним, поэтому merch.Employee пустой, надо запрашивать внешний сервис в цикле.
            var employees = await _employeeWithMerchsDomainService.FindAllBy(shippedSkuList, token);

            var tasks = new List<Task>();

            foreach (var employee in employees)
            {
                foreach (var merch in employee.Merchs)
                {
                    if (merch.Mode == MerchMode.Manual)
                    {
                        tasks.Add(
                            _emailServiceMock.SendCanPickupMerchNotification(
                                merch.Employee.Name.Value,
                                merch.Employee.Email.Value));
                    }
                    else if (merch.Mode == MerchMode.Auto)
                    {
                        tasks.Add(
                            GiveOutMerch(merch, token));
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(nameof(merch.Mode));
                    }
                }
            }

            await Task.WhenAll(tasks);

            return employees
                .SelectMany(e => e.Merchs)
                .Where(e => e.Status == MerchStatus.Done)
                .ToArray();
        }

        private async Task<Merch> GiveOutMerch(Merch merch, CancellationToken token)
        {
            if (await _stockApiClientMock.TryReserve(merch.MerchPack.SkuListValues, token))
            {
                merch.Done();
                await _merchRepository.Save(merch, token);

                if (merch.Mode == MerchMode.Auto)
                {
                    await _emailServiceMock.SendPickupMerchNotification(
                        merch.Employee.Name.Value,
                        merch.Employee.Email.Value);
                }
            }
            else
            {
                merch.Waiting();
                await _merchRepository.Save(merch, token);

                // TODO: где найти email и имя HR?
                await _emailServiceMock.SendMerchOutOfStockNotification("", "", merch.MerchPack.MerchType.Value);
            }

            return merch;
        }
    }
}
