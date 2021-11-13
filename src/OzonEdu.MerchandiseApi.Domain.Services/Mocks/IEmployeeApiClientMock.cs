using System.Threading;
using System.Threading.Tasks;

namespace OzonEdu.MerchandiseApi.Domain.Services.Mocks
{
    public interface IEmployeeApiClientMock
    {
        Task<EmployeeDto> GetBy(string employeeEmail, CancellationToken token);
    }

    public sealed class EmployeeApiClientMock : IEmployeeApiClientMock
    {
        public Task<EmployeeDto> GetBy(string employeeEmail, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed class EmployeeDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string ClothingSize { get; set; }
    }
}
