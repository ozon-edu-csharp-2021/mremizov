using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Npgsql;

namespace OzonEdu.MerchandiseApi.Domain.Repositories.Infrastructure
{
    public sealed class NpgsqlConnectionFactory : IDbConnectionFactory<NpgsqlConnection>
    {
        private readonly DbConfiguration _options;
        private NpgsqlConnection _connection;

        public NpgsqlConnectionFactory(IOptions<DbConfiguration> options)
        {
            _options = options.Value;
        }

        public async Task<NpgsqlConnection> CreateConnection(CancellationToken token)
        {
            if (_connection != null)
            {
                return _connection;
            }

            _connection = new NpgsqlConnection(_options.ConnectionString);

            await _connection.OpenAsync(token);

            _connection.StateChange += (o, e) =>
            {
                if (e.CurrentState == ConnectionState.Closed)
                {
                    _connection = null;
                }
            };

            return _connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
