using Microsoft.Extensions.DependencyInjection;
using OzonEdu.MerchandiseApi.Domain.Common;

namespace OzonEdu.MerchandiseApi.Domain.Repositories
{
    public static class Setup
    {
        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(classes => classes.AssignableTo<IRepository>())
                .AsMatchingInterface()
                .WithTransientLifetime());

            return serviceCollection;
        }
    }
}
