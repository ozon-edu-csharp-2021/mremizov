using System.IO;
using System.Linq;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace OzonEdu.MerchandiseApi.Migrator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetSection("DbConfiguration:ConnectionString").Get<string>();

            var services = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(options => options
                    .AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(Program).Assembly)
                    .For.Migrations())
                .AddLogging(options => options
                    .AddFluentMigratorConsole());

            var serviceProvider = services.BuildServiceProvider(false);

            using (serviceProvider.CreateScope())
            {
                var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

                if (args.Contains("--dryrun"))
                {
                    runner.ListMigrations();
                }
                else
                {
                    runner.MigrateUp();
                }

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    connection.ReloadTypes();
                }
            }
        }
    }
}
