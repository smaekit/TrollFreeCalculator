using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using TollFeeCalculator;
using TollFreeCalculator.Interfaces;
using TollFreeCalculator.Models;

namespace TollFreeCalculator
{
    class Program
    {
        private static IConfigurationRoot _configuration;
        static void Main(string[] args)
        {
            BuildConfiguration();
            IAppConfiguration appConfiguration = new AppConfiguration(_configuration);

            ITollCalculatorService tollCalcService = new TollCalculator();
            var feeCost = tollCalcService.GetTollFee(vehicle: new Car(), new List<DateTime>() { new DateTime(2020, 11, 30, 6, 0, 0), new DateTime(2020, 11, 30, 6, 30, 0), new DateTime(2020, 11, 30, 12, 0, 0), new DateTime(2020, 11, 30, 17, 0, 0) });//, new DateTime(2020,12,31,19,0,0) });
            Console.WriteLine("Hello World! FeeCost is: " + feeCost);
        }
        private static void BuildConfiguration()
        {
            var EnviromentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{EnviromentName}.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();
        }
    }
}
