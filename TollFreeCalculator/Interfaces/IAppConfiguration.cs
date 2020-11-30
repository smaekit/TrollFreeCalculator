using TollFreeCalculator.Util;

namespace TollFreeCalculator.Models
{
    public interface IAppConfiguration
    {
        public FeeConfiguration FeeConfiguration { get; }
        public FeeAtTimeConfiguration FeeAtTimeConfiguration { get; }
    }
}