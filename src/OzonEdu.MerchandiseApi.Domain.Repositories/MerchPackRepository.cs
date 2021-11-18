using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.MerchandiseApi.Domain.Entities;
using OzonEdu.MerchandiseApi.Domain.Enumerations;
using OzonEdu.MerchandiseApi.Domain.Exceptions;
using OzonEdu.MerchandiseApi.Domain.Repositories.Infrastructure;
using OzonEdu.MerchandiseApi.Domain.Repositories.Models;
using OzonEdu.MerchandiseApi.Domain.ValueObjects;

namespace OzonEdu.MerchandiseApi.Domain.Repositories
{
    public sealed class MerchPackRepository : IMerchPackRepository
    {
        private const int CommandTimeout = 30;

        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;

        public MerchPackRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<MerchPack> FindBy(CSharpCourse.Core.Lib.Enums.MerchType merchType, ClothingSize clothingSize, CancellationToken token)
        {
            var sql = @"
                SELECT 	id, type, items
                FROM	public.merch_packs
                WHERE	type = @MerchType
		                AND (clothing_size IS NULL OR clothing_size = @ClothingSize);";

            var parameters = new
            {
                MerchType = merchType,
                ClothingSize = clothingSize.Id
            };

            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: CommandTimeout,
                cancellationToken: token);

            var connection = await _dbConnectionFactory.CreateConnection(token);

            var merchPack = await connection.QueryFirstOrDefaultAsync<MerchPackModel>(commandDefinition);

            if (merchPack == null)
            {
                throw new MerchPackNotFoundException();
            }

            return new MerchPack(
                merchPack.Id,
                new MerchType(merchPack.Type),
                new SkuList(merchPack.Items.Select(e => new Sku(long.Parse(e)))));
        }
    }
}
