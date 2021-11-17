using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.MerchandiseApi.Domain.Entities;
using OzonEdu.MerchandiseApi.Domain.Enumerations;
using OzonEdu.MerchandiseApi.Domain.Repositories.Infrastructure;
using OzonEdu.MerchandiseApi.Domain.Repositories.Models;
using OzonEdu.MerchandiseApi.Domain.ValueObjects;

namespace OzonEdu.MerchandiseApi.Domain.Repositories
{
    public sealed class MerchRepository : IMerchRepository
    {
        private const int CommandTimeout = 30;

        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;

        public MerchRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<Merch>> FindAllBy(Employee employee, CancellationToken token)
        {
            var sql = @"
                SELECT  m.id, m.created_utc, m.status, m.mode,
                        mp.id as merchpack_id, mp.type as merchpack_type, mp.items as merchpack_items
	            FROM    public.merchs m
                            JOIN public.merch_packs mp ON mp.id = m.merchpack_id
                WHERE   m.employee_id = @EmployeeId;";

            var parameters = new
            {
                EmployeeId = employee.Id
            };

            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: CommandTimeout,
                cancellationToken: token);

            var connection = await _dbConnectionFactory.CreateConnection(token);

            var merchs = await connection.QueryAsync<
                MerchModel, MerchPackModel, Merch>(commandDefinition,
                (merch, merchPack) => new Merch(
                    merch.Id,
                    merch.CreatedUtc,
                    MerchMode.GetBy(merch.Mode),
                    MerchStatus.GetBy(merch.Status),
                    employee,
                    new MerchPack(
                        merchPack.Id,
                        new MerchType(merchPack.Type),
                        new SkuList(merchPack.Items.Select(e => new Sku(e))))));

            return merchs;
        }

        public async Task<IEnumerable<Merch>> FindAllBy(IEnumerable<long> skuList, CancellationToken token)
        {
            // TODO: where in for jsonb array of long
            var sql = @"
                SELECT  m.id, m.created_utc, m.status, m.mode,
                        mp.id as merchpack_id, mp.type as merchpack_type, mp.items as merchpack_items,
                        m.employee_id
	            FROM    public.merchs m
                            JOIN public.merch_packs mp ON mp.id = m.merchpack_id
                WHERE   // TODO: where in for jsonb array of long";

            var parameters = new
            {
                SkuList = skuList
            };

            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: CommandTimeout,
                cancellationToken: token);

            var connection = await _dbConnectionFactory.CreateConnection(token);

            var merchs = await connection.QueryAsync<
                MerchModel, MerchPackModel, EmployeeModel, Merch>(commandDefinition,
                (merch, merchPack, employee) => new Merch(
                    merch.Id,
                    merch.CreatedUtc,
                    MerchMode.GetBy(merch.Mode),
                    MerchStatus.GetBy(merch.Status),
                    new Employee(employee.Id),
                    new MerchPack(
                        merchPack.Id,
                        new MerchType(merchPack.Type),
                        new SkuList(merchPack.Items.Select(e => new Sku(e))))));

            return merchs;
        }

        public async Task Save(Merch merch, CancellationToken token)
        {
            if (merch.Id > 0)
            {
                var sql = @"
                    UPDATE  merchs
                    SET     status = @Status
                    WHERE   id = @Id;";

                var parameters = new
                {
                    Id = merch.Id,
                    Status = merch.Status
                };

                var commandDefinition = new CommandDefinition(
                    sql,
                    parameters: parameters,
                    commandTimeout: CommandTimeout,
                    cancellationToken: token);

                var connection = await _dbConnectionFactory.CreateConnection(token);

                await connection.ExecuteAsync(commandDefinition);
            }
            else
            {
                var sql = @"
                    INSERT INTO public.merchs(
	                    merchpack_id, employee_id, created_utc, status, mode)
	                VALUES (@MerchpackId, @EmployeeId, @CreatedUtc, @Status, @Mode);";

                var parameters = new
                {
                    MerchpackId = merch.MerchPack.Id,
                    EmployeeId = merch.Employee.Id,
                    CreatedUtc = merch.CreatedUtc,
                    Status = merch.Status,
                    Mode = merch.Mode
                };

                var commandDefinition = new CommandDefinition(
                    sql,
                    parameters: parameters,
                    commandTimeout: CommandTimeout,
                    cancellationToken: token);

                var connection = await _dbConnectionFactory.CreateConnection(token);

                await connection.ExecuteAsync(commandDefinition);
            }
        }
    }
}
