using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Domain.Common;
using OzonEdu.MerchandiseApi.Domain.Entities;

namespace OzonEdu.MerchandiseApi.Domain.Services
{
    public interface IEmployeeDomainService : IDomainService
    {
        Task<Employee> FindBy(EmployeeParameters parameters, CancellationToken token);
    }
}
