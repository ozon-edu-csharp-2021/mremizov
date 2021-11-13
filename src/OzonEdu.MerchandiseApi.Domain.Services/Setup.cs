using Microsoft.Extensions.DependencyInjection;
using OzonEdu.MerchandiseApi.Domain.Common;

namespace OzonEdu.MerchandiseApi.Domain.Services
{
    public static class Setup
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(classes => classes.AssignableTo<IDomainService>())
                .AsMatchingInterface()
                .WithTransientLifetime());

            return serviceCollection;
        }
    }
}
