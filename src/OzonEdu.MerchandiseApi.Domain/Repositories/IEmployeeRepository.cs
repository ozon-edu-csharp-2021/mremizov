using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Domain.Entities;
using OzonEdu.MerchandiseApi.Domain.Services;

namespace OzonEdu.MerchandiseApi.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> FindBy(EmployeeParameters search, CancellationToken token);

        Task Save(Employee employee, CancellationToken token);
    }
}
