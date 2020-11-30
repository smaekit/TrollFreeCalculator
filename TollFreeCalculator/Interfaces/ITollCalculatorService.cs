using System;
using System.Collections.Generic;
using System.Text;
using TollFeeCalculator;

namespace TollFreeCalculator.Interfaces
{
    public interface ITollCalculatorService
    {
        /// <summary>
        /// Calculate total tolling fee for one day
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="dates"></param>
        /// <returns></returns>
        int GetTollFee(Vehicle vehicle, List<DateTime> dates);
    }
}
