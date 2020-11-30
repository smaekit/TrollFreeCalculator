using Nager.Date;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TollFeeCalculator;
using TollFreeCalculator.Interfaces;
using TollFreeCalculator.Models;

public class TollCalculator : ITollCalculatorService
{

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */
 
    public int GetTollFee(Vehicle vehicle, List<DateTime> dates)
    {
        DateTime intervalStart = dates[0];
        int intervalFee = 0;
        int totalFee = 0;

        var IsValidRangeOfDates = dates.Any(x => x.Date != intervalStart.Date) ? false : true;
        if (!IsValidRangeOfDates)
            throw new Exception("Please provide tolling fees for just one day. Not allowed to Hack the world!");

        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee(date, vehicle);

            if (intervalStart.AddMinutes(Globals.AppConfiguration.FeeConfiguration.FeeIntervalInMinutes) >= date)  //Är avgiften inom tidsram för 1h
            {
                if (totalFee > 0)
                {
                    totalFee -= intervalFee;        //Ta bort nuvarande avgift förelagd inom intervallstiden
                }

                if (nextFee >= intervalFee)
                {
                    intervalFee = nextFee;          //Om nuvarande avgift är större så ska den dras av
                }
                totalFee += intervalFee;            //Annars dra av tidigare högsta avgift
            }
            else                                    //Ny avgift
            {
                intervalStart = date;               //Ny tidpunkt för intervall 
                intervalFee = nextFee;
                totalFee += nextFee;
            }
        }
        int maxTotalFee = Globals.AppConfiguration.FeeConfiguration.MaxTotalFee;
        if (totalFee > maxTotalFee) totalFee = maxTotalFee;
        return totalFee;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="vehicle"></param>
    /// <returns></returns>
    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        if (vehicle == null) return false;
        String vehicleType = vehicle.GetVehicleType();
        return vehicleType.Equals(TollFreeVehicles.Motorbike.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Military.ToString());
    }

    /// <summary>
    /// Converte timezone into toll fee
    /// </summary>
    /// <param name="date"></param>
    /// <param name="vehicle"></param>
    /// <returns></returns>
    private int GetTollFee(DateTime date, Vehicle vehicle)
    {
        if (IsTollFreeVehicle(vehicle) || IsTollFreeDate(date)) return 0;

        int hour = date.Hour;
        int minute = date.Minute;

        if (hour == 6 && minute >= 0 && minute <= 29) return Globals.AppConfiguration.FeeAtTimeConfiguration.TimeFeeAt6a;
        else if (hour == 6 && minute >= 30 && minute <= 59) return Globals.AppConfiguration.FeeAtTimeConfiguration.TimeFeeAt6b;
        else if (hour == 7 && minute >= 0 && minute <= 59) return Globals.AppConfiguration.FeeAtTimeConfiguration.TimeFeeAt7;
        else if (hour == 8 && minute >= 0 && minute <= 29) return Globals.AppConfiguration.FeeAtTimeConfiguration.TimeFeeAt8a;
        else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return Globals.AppConfiguration.FeeAtTimeConfiguration.TimeFeeAt8b;
        else if (hour == 15 && minute >= 0 && minute <= 29) return Globals.AppConfiguration.FeeAtTimeConfiguration.TimeFeeAt15a;
        else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return Globals.AppConfiguration.FeeAtTimeConfiguration.TimeFeeAt15b;
        else if (hour == 17 && minute >= 0 && minute <= 59) return Globals.AppConfiguration.FeeAtTimeConfiguration.TimeFeeAt17;
        else if (hour == 18 && minute >= 0 && minute <= 29) return Globals.AppConfiguration.FeeAtTimeConfiguration.TimeFeeAt18;
        else return Globals.AppConfiguration.FeeAtTimeConfiguration.NoFee;
    }

    /// <summary>
    /// https://github.com/nager/Nager.Date
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    private Boolean IsTollFreeDate(DateTime date)
    {       
        if (IsWeekEnd(date)) 
            return true;

        var publicHolidays = DateSystem.GetPublicHoliday(date.Year, "SE");
        if (!publicHolidays.Any(x => x.Date == new DateTime(date.Year, 12, 31)))
            throw new Exception("Cannot verify public holidays please contact ***Hackers from 1995***");

        if (publicHolidays.Any(x => x.Date == date.Date)) 
            return true;

        return false;
    }

    /// <summary>
    /// Helg?
    /// </summary>
    /// <param name="timeOfToll"></param>
    /// <returns></returns>
    private bool IsWeekEnd(DateTime timeOfToll) =>
    timeOfToll.DayOfWeek switch
    {
        DayOfWeek.Saturday => true,
        DayOfWeek.Sunday => true,
        _ => false
    };

    private enum TollFreeVehicles
    {
        Motorbike = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5
    }
}