using System;
using System.Threading;
using System.Threading.Tasks;

namespace OzonEdu.MerchandiseApi.Domain.Repositories.Infrastructure
{
    public interface IDbConnectionFactory<TConnection> : IDisposable
    {
        Task<TConnection> CreateConnection(CancellationToken token);
    }
}
