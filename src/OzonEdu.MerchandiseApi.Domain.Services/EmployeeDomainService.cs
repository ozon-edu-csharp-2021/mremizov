using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Domain.Entities;
using OzonEdu.MerchandiseApi.Domain.Enumerations;
using OzonEdu.MerchandiseApi.Domain.Exceptions;
using OzonEdu.MerchandiseApi.Domain.Repositories;
using OzonEdu.MerchandiseApi.Domain.Services.Mocks;

namespace OzonEdu.MerchandiseApi.Domain.Services
{
    public sealed class EmployeeDomainService : IEmployeeDomainService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeApiClientMock _employeeApiClientMock;

        public EmployeeDomainService(
            IEmployeeRepository employeeRepository,
            IEmployeeApiClientMock employeeApiClientMock)
        {
            _employeeRepository = employeeRepository;
            _employeeApiClientMock = employeeApiClientMock;
        }

        public async Task<Employee> FindOrCreateBy(EmployeeParameters parameters, CancellationToken token)
        {
            var employee = await _employeeRepository.FindBy(parameters, token);

            if (employee == null)
            {
                var employeeDto = await _employeeApiClientMock.GetBy(
                    parameters.EmployeeEmail,
                    parameters.EmployeeName,
                    token);

                if (employeeDto != null)
                {
                    employee = new Employee(
                        new ValueObjects.Name(employeeDto.Name),
                        new ValueObjects.Email(employeeDto.Email),
                        ClothingSize.GetBy(employeeDto.ClothingSize));

                    await _employeeRepository.Save(employee, token);
                }
            }

            if (employee == null)
            {
                throw new EmployeeNotFoundException();
            }

            return employee;
        }
    }
}
