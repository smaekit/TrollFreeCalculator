using System;
using System.Collections.Generic;
using System.Text;

namespace TollFreeCalculator.Util
{
    /// <summary>
    /// Fee at different time intervals
    /// </summary>
    public class FeeAtTimeConfiguration
    {
        public int TimeFeeAt6a { get; set; }
        public int TimeFeeAt6b { get; set; }
        public int TimeFeeAt7 { get; set; }
        public int TimeFeeAt8a { get; set; }
        public int TimeFeeAt8b { get; set; }
        public int TimeFeeAt15a { get; set; }
        public int TimeFeeAt15b { get; set; }
        public int TimeFeeAt17 { get; set; }
        public int TimeFeeAt18 { get; set; }
        public int NoFee { get; set; }
    }
}
