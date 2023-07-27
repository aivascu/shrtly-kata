using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ShrtLy.DAL;

namespace ShrtLy.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .Configure<Startup>(Directory.GetCurrentDirectory())
                .Build();

            DatabaseInitializer.ApplyMigrations<ShrtLyContext>(
                configuration.GetConnectionString("ShrtLy"));

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
