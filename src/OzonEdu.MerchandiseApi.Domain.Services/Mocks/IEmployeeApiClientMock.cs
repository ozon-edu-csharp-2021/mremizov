using System.Threading;
using System.Threading.Tasks;

namespace OzonEdu.MerchandiseApi.Domain.Services.Mocks
{
    public interface IEmployeeApiClientMock
    {
        Task<EmployeeDto> GetBy(string employeeEmail, string employeeName, CancellationToken token);
    }

    public sealed class EmployeeDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string ClothingSize { get; set; }
    }
}
