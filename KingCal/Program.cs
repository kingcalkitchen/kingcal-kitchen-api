using KingCal.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace KingCal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "KingCal Kitchen";

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                .AddCommandLine(args)
                .Build();

            //var appSettingsSection = config.GetSection("AppSettings");
            //var appSettings = appSettingsSection.Get<AppSettings>();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config).CreateLogger();

            try
            {
                Log.Information("Application Starting Up");
                Log.Information("******************************************************************");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The application failed to start correctly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel();
                    webBuilder.UseSerilog();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
