using Microsoft.Extensions.Configuration;
using TollFreeCalculator.Models;
using TollFreeCalculator.Util;

namespace TollFreeCalculator
{
    internal class AppConfiguration : IAppConfiguration
    {
        public AppConfiguration(IConfigurationRoot configuration)
        {
            this.FeeConfiguration = new FeeConfiguration
            {
                FeeIntervalInMinutes = int.Parse(configuration.GetSection("FeeConfiguration:FeeIntervalInMinutes").Value.ToString()),
                MaxTotalFee = int.Parse(configuration.GetSection("FeeConfiguration:MaxTotalFee").Value.ToString())
            };

            this.FeeAtTimeConfiguration = new FeeAtTimeConfiguration
            {
                TimeFeeAt6a = int.Parse(configuration.GetSection("FeeAtTimeConfiguration:TimeFeeAt6a").Value.ToString()),
                TimeFeeAt6b = int.Parse(configuration.GetSection("FeeAtTimeConfiguration:TimeFeeAt6b").Value.ToString()),
                TimeFeeAt7 = int.Parse(configuration.GetSection("FeeAtTimeConfiguration:TimeFeeAt7").Value.ToString()),
                TimeFeeAt8a = int.Parse(configuration.GetSection("FeeAtTimeConfiguration:TimeFeeAt8a").Value.ToString()),
                TimeFeeAt8b = int.Parse(configuration.GetSection("FeeAtTimeConfiguration:TimeFeeAt8b").Value.ToString()),
                TimeFeeAt15a = int.Parse(configuration.GetSection("FeeAtTimeConfiguration:TimeFeeAt15a").Value.ToString()),
                TimeFeeAt15b = int.Parse(configuration.GetSection("FeeAtTimeConfiguration:TimeFeeAt15b").Value.ToString()),
                TimeFeeAt17 = int.Parse(configuration.GetSection("FeeAtTimeConfiguration:TimeFeeAt17").Value.ToString()),
                TimeFeeAt18 = int.Parse(configuration.GetSection("FeeAtTimeConfiguration:TimeFeeAt18").Value.ToString()),
                NoFee = int.Parse(configuration.GetSection("FeeAtTimeConfiguration:NoFee").Value.ToString())
            };
            Globals.AppConfiguration = this;
        }

        /// <summary>
        /// Configuration av tidsspann för avgiftsbeläggning och maxgräns
        /// </summary>
        public FeeConfiguration FeeConfiguration { get; set; }

        /// <summary>
        /// Avgift vid tidpunkt
        /// </summary>
        public FeeAtTimeConfiguration FeeAtTimeConfiguration { get; set; }
    }
}