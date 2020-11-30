using System;
using System.Collections.Generic;
using System.Text;

namespace TollFreeCalculator.Models
{
    /// <summary>
    /// Fee configuration attributes
    /// </summary>
    public class FeeConfiguration
    {
        public int FeeIntervalInMinutes { get; set; }
        public int MaxTotalFee { get; set; }
    }
}
