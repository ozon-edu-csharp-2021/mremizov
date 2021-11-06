using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Domain.Entities;
using OzonEdu.MerchandiseApi.Domain.Services;

namespace OzonEdu.MerchandiseApi.Domain.Repositories
{
    public sealed class EmployeeRepository : IEmployeeRepository
    {
        public Task<Employee> FindBy(EmployeeParameters search, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task Save(Employee employee, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}
