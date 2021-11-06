using Microsoft.Extensions.DependencyInjection;

namespace OzonEdu.MerchandiseApi.Domain.Repositories
{
    public static class Setup
    {
        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IEmployeeRepository, EmployeeRepository>();
            serviceCollection.AddTransient<IMerchPackRepository, MerchPackRepository>();
            serviceCollection.AddTransient<IMerchRepository, MerchRepository>();

            return serviceCollection;
        }
    }
}
