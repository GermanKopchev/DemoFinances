﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoFinances
{
    internal class Calculate
    {
        public Dictionary<int,double> intrplatedRate()
        {
            Dictionary<int, double> dictionary = new Dictionary<int, double>();
            dictionary.Add(3, 0.03);
            dictionary.Add(6, 0.0333);
            dictionary.Add(12, 0.04);
            dictionary.Add(18, 0.041);
            dictionary.Add(24, 0.042);
            dictionary.Add(30, 0.0435);
            dictionary.Add(36, 0.045);
            dictionary.Add(42, 0.0475);
            dictionary.Add(48, 0.05);
            return dictionary;
        }

        public List<DateTime> datesList(DateTime startDate, DateTime endDate, int interestPeriod)
        {
            List<DateTime> dates = new List<DateTime>();
            while (startDate < endDate)
            {
                dates.Insert(0, endDate);
                endDate = endDate.AddMonths(-interestPeriod).Date;
            }
            dates.Insert(0, startDate);
            return dates;
        }

        public double Days360(DateTime startDate, DateTime endDate, bool europenOrUSMethod)
        {
            int dayStart = startDate.Day;
            int dayEnd = endDate.Day;

            if(europenOrUSMethod)
            {
                if(dayStart == 31) {
                    dayStart = 30;
                }
                if(dayEnd == 31) {
                    dayEnd = 30;
                }
            }
            else
            {
                if(IsLastDayOfFebruary(startDate) && IsLastDayOfFebruary(endDate)) {
                    dayEnd = 30;
                }
                if(dayStart == 31 || IsLastDayOfFebruary(startDate)) {
                    dayStart = 30;
                }
                if(dayStart == 30 && dayEnd == 31) { 
                    dayEnd = 30;
                }
            }
            return (endDate.Year - startDate.Year) * 360 + (endDate.Month - startDate.Month) * 30 + (dayEnd - dayStart);
        }

        public bool IsLastDayOfFebruary(DateTime date)
        {
            return date.Month == 2 && date.Day == DateTime.DaysInMonth(date.Year, date.Month);
        }

        public void calculateBond(Bond bond)
        {
            List<DateTime> bondDates = new List<DateTime>();
            bondDates = datesList(bond.StartDate, bond.EndDate, bond.InterestPeriod);
            int redemption = 0;
            double interestRatePayment;
            double totalPayment;
            double distance;
            long capital = bond.Nominal;
            double discoutFactor;
            double dtPeriod;
            Dictionary<int, double> intrplatedRates = intrplatedRate();
            double value;
            double totalValue = 0;
            double cleanValue = 0;
            DateTime dateOneYearBefore;
            double accruedInterest = 0;
            for (int i = 1; i < bondDates.Count; i++)
            {
                capital -= redemption;
                if (i == bondDates.Count - 1)
                {
                    redemption = 100000;
                }
                dateOneYearBefore = new DateTime(bondDates[i].Year - 1, bondDates[i].Month, bondDates[i].Day);
                dtPeriod = Days360(dateOneYearBefore, bondDates[i], true) / 360;
                interestRatePayment = bond.InterestRate * capital * dtPeriod;
                totalPayment = interestRatePayment + redemption;
                distance = Days360(bond.StartDate, bondDates[i], true) / 360;
                if (distance <= 1)
                {
                    discoutFactor = 1 / (1 + distance * intrplatedRates[Convert.ToInt32(distance * 12)]);
                }
                else
                {
                    discoutFactor = 1 / Math.Pow(1 + distance, intrplatedRates[Convert.ToInt32(distance * 12)]);
                }
                value = totalPayment * discoutFactor;
                totalValue += value;
                if (distance >= 1)
                {
                    cleanValue += value;
                }
                if (Convert.ToDouble(bond.InterestPeriod / 12) > distance)
                {
                    accruedInterest = interestRatePayment * (1 - distance);
                }

            }
            double cleanPrice = cleanValue / bond.Nominal * 100;
            double dirtyValue = cleanPrice + accruedInterest;
            double dirtyPrice = dirtyValue / bond.Nominal * 100;
        }

        public void calculateFloaterBond(Bond bond)
        {

        }
    }
}