using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FashopBackend.Infrastructure.Data;

namespace FashopBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting Fashop Backend Server");
            CreateHostBuilder(args)
                .Build()
                .MigrateDatabase<FashopContext>()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var port = Environment.GetEnvironmentVariable("PORT");
                    webBuilder.UseStartup<Startup>().UseUrls("http://*:" + port);
                });
    }
}
