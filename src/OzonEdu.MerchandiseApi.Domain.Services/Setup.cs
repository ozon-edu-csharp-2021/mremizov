using Microsoft.Extensions.DependencyInjection;

namespace OzonEdu.MerchandiseApi.Domain.Services
{
    public static class Setup
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IEmployeeDomainService, EmployeeDomainService>();
            serviceCollection.AddTransient<IEmployeeWithMerchsDomainService, EmployeeWithMerchsDomainService>();
            serviceCollection.AddTransient<IMerchDomainService, MerchDomainService>();
            serviceCollection.AddTransient<IMerchPackDomainService, MerchPackDomainService>();

            return serviceCollection;
        }
    }
}
