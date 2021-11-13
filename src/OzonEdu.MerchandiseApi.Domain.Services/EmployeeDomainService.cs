using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Domain.Entities;
using OzonEdu.MerchandiseApi.Domain.Enumerations;
using OzonEdu.MerchandiseApi.Domain.Exceptions;
using OzonEdu.MerchandiseApi.Domain.Services.Mocks;
using OzonEdu.MerchandiseApi.Domain.ValueObjects;

namespace OzonEdu.MerchandiseApi.Domain.Services
{
    public sealed class EmployeeDomainService : IEmployeeDomainService
    {
        private readonly IEmployeeApiClientMock _employeeApiClientMock;

        public EmployeeDomainService(
            IEmployeeApiClientMock employeeApiClientMock)
        {
            _employeeApiClientMock = employeeApiClientMock;
        }

        public async Task<Employee> FindBy(EmployeeParameters parameters, CancellationToken token)
        {
            var employeeDto = await _employeeApiClientMock.GetBy(
                    parameters.EmployeeEmail,
                    token);

            if (employeeDto == null)
            {
                throw new EmployeeNotFoundException();
            }

            return new Employee(
                new Name(employeeDto.Name),
                new Email(employeeDto.Email),
                ClothingSize.GetBy(employeeDto.ClothingSize));
        }
    }
}
