using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Domain.EmployeeAggregate;

namespace OzonEdu.MerchandiseApi.Domain.Services.Mocks
{
    public interface IEmployeeApiClientMock
    {
        Task<Employee> GetBy(long employeeId, CancellationToken token);

        Task<Employee> GetBy(string employeeEmail, string employeeName, CancellationToken token);
    }
}
