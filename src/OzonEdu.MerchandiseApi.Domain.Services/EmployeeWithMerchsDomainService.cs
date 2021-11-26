using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Domain.Aggregates;
using OzonEdu.MerchandiseApi.Domain.Repositories;

namespace OzonEdu.MerchandiseApi.Domain.Services
{
    public sealed class EmployeeWithMerchsDomainService : IEmployeeWithMerchsDomainService
    {
        private readonly IMerchRepository _merchRepository;
        private readonly IEmployeeDomainService _employeeDomainService;

        public EmployeeWithMerchsDomainService(
            IMerchRepository merchRepository,
            IEmployeeDomainService employeeDomainService)
        {
            _merchRepository = merchRepository;
            _employeeDomainService = employeeDomainService;
        }

        public async Task<EmployeeWithMerchs> FindBy(EmployeeParameters parameters, CancellationToken token)
        {
            var employee = await _employeeDomainService.FindBy(parameters, token);

            var merchs = await _merchRepository.FindAllBy(employee, token);

            return new EmployeeWithMerchs(employee, merchs);
        }

        public async Task<IEnumerable<EmployeeWithMerchs>> FindAllBy(IEnumerable<long> skuList, CancellationToken token)
        {
            var merchs = await _merchRepository.FindAllBy(skuList, token);

            return merchs
                .GroupBy(e => e.Employee)
                .Select(g =>
                {
                    return new EmployeeWithMerchs(g.Key, g);
                })
                .ToArray();
        }
    }
}
