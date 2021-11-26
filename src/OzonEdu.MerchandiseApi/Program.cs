using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using OzonEdu.MerchandiseApi.Infrastructure;

namespace OzonEdu.MerchandiseApi
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            Migrator.Program.Main(new string[0]);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                    {
                        // HTTP
                        options.ListenAnyIP(80, listenOptions =>
                        {
                            listenOptions.Protocols = HttpProtocols.Http1;
                        });

                        // GRPC
                        options.ListenAnyIP(8080, listenOptions =>
                        {
                            listenOptions.Protocols = HttpProtocols.Http2;
                        });
                    });

                    webBuilder.UseStartup<Startup>();
                })
                .AddInfrastructure();
    }
}
