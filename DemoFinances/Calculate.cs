﻿namespace DemoFinances
{

    internal class Calculate
    {
        //bond interplated rate
        public Dictionary<int, double> intrplatedRate()
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

        public double Days360(DateTime startDate, DateTime endDate, day360Method method)
        {
            int dayStart = startDate.Day;
            int dayEnd = endDate.Day;

            if (method == day360Method.Europion)
            {
                if (dayStart == 31)
                {
                    dayStart = 30;
                }
                if (dayEnd == 31)
                {
                    dayEnd = 30;
                }
            }
            else
            {
                if (IsLastDayOfFebruary(startDate) && IsLastDayOfFebruary(endDate))
                {
                    dayEnd = 30;
                }
                if (dayStart == 31 || IsLastDayOfFebruary(startDate))
                {
                    dayStart = 30;
                }
                if (dayStart == 30 && dayEnd == 31)
                {
                    dayEnd = 30;
                }
            }
            return (endDate.Year - startDate.Year) * 360 + (endDate.Month - startDate.Month) * 30 + (dayEnd - dayStart);
        }

        public bool IsLastDayOfFebruary(DateTime date)
        {
            return date.Month == 2 && date.Day == DateTime.DaysInMonth(date.Year, date.Month);
        }

        public List<BondCalculationForPeriod> calculateBond(Bond bond)
        {
            List<BondCalculationForPeriod> list = new List<BondCalculationForPeriod>();
            List<DateTime> bondDates = datesList(bond.StartDate, bond.EndDate, bond.InterestPeriod);
            double redemption = 0;
            switch(bond.BondType)
            {
                case BondType.Regular:
                    
                    break;
                case BondType.Bullet:
                    redemption = Math.Round(Convert.ToDouble(bond.Nominal / (bondDates.Count - 1)), 2);
                    break;
                case BondType.Annuity:

                    break;
                default: 
                    break;
            }
            double capital = bond.Nominal;
            for (int i = 1; i < bondDates.Count; i++)
            {
                if (i == bondDates.Count - 1 && bond.BondType == BondType.Regular)
                {
                    redemption = Convert.ToInt32(capital);
                }
                DateTime dateOneYearBefore = new DateTime(bondDates[i].Year - 1, bondDates[i].Month, bondDates[i].Day);
                double dtPeriod = Days360(dateOneYearBefore, bondDates[i], day360Method.Europion) / 360;
                BondCalculationForPeriod bondCalculation = new BondCalculationForPeriod(bondDates[i - 1], bondDates[i], bond.InterestRate, redemption, capital, dtPeriod);
                capital -= redemption;
                list.Add(bondCalculation);
            }
            advancedBondCalculation(list);
            return list;
        }

        public void advancedBondCalculation(List<BondCalculationForPeriod> list)
        {
            Dictionary<int, double> intrplatedRates = intrplatedRate();
            foreach (BondCalculationForPeriod bondCalculation in list)
            {
                bondCalculation.InterestRatePayment = bondCalculation.InterestRate * bondCalculation.Capital * bondCalculation.DTPeriod;
                bondCalculation.TotalPayment = bondCalculation.InterestRatePayment + bondCalculation.Redemption;
                bondCalculation.Distance = Days360(list.First().StartDate, bondCalculation.EndDate, day360Method.Europion) / 360;
                if (bondCalculation.Distance <= 1)
                {
                    bondCalculation.DiscountFactors = 1 / (1 + bondCalculation.Distance * intrplatedRates[Convert.ToInt32(bondCalculation.Distance * 12)]);
                }
                else
                {
                    bondCalculation.DiscountFactors = 1 / Math.Pow(1 + bondCalculation.Distance, intrplatedRates[Convert.ToInt32(bondCalculation.Distance * 12)]);
                }
                bondCalculation.Value = bondCalculation.TotalPayment * bondCalculation.DiscountFactors;
            }
        }

        //Evaluation
        public Dictionary<int, double> zeroRiskInterestCurve()
        {
            Dictionary<int, double> dictionary = new Dictionary<int, double>();
            dictionary.Add(6, 0.034);
            dictionary.Add(12, 0.034);
            dictionary.Add(18, 0.034);
            dictionary.Add(24, 0.036);
            dictionary.Add(30, 0.036);
            dictionary.Add(36, 0.036);
            dictionary.Add(42, 0.036);
            dictionary.Add(48, 0.038);
            dictionary.Add(54, 0.038);
            dictionary.Add(60, 0.038);
            dictionary.Add(66, 0.038);
            dictionary.Add(72, 0.038);
            dictionary.Add(78, 0.038);
            dictionary.Add(84, 0.04);
            dictionary.Add(90, 0.04);
            dictionary.Add(96, 0.04);
            return dictionary;
        }

        public Dictionary<int, double> scenarioCurve(double add)
        {
            Dictionary<int, double> dictionary = zeroRiskInterestCurve();
            foreach (KeyValuePair<int, double> pair in dictionary)
            {
                dictionary[pair.Key] = pair.Value + add;
            }
            return dictionary;
        }

        public List<double> discoutFactors(int scenario)
        {
            Dictionary<int, double> scenCurve = scenarioCurve(scenario);
            List<double> discoutFactors = new List<double>();
            foreach (KeyValuePair<int, double> pair in scenCurve)
            {
                discoutFactors.Add((1 / (1 + pair.Value * pair.Key / 12)));
            }
            discoutFactors.Insert(0, 1);
            return discoutFactors;
        }

        public void calculateFloaterBond(Bond bond)
        {
            List<DateTime> bondDates = new List<DateTime>();
            double amortization = 0;
            double capital = bond.Nominal;
            List<double> dicountFactors = discoutFactors(30);
            double netPresentValue = 0;
            double durationInYears = 0;
            for (int i = 1; i < bondDates.Count; i++)
            {
                DateTime dateOneYearBefore = new DateTime(bondDates[i].Year - 1, bondDates[i].Month, bondDates[i].Day);
                double periodLength = Days360(dateOneYearBefore, bondDates[i], day360Method.Europion) / 360;
                double distance = Days360(bond.StartDate, bondDates[i], day360Method.Europion) / 360;
                capital -= amortization;
                if (i == bondDates.Count - 1)
                {
                    amortization = capital;
                }
                double interestRate = (dicountFactors[i - 1] / dicountFactors[i] - 1) / periodLength;
                double interestPay = periodLength * capital * interestRate;
                double totalPay = amortization + interestPay;
                double value = (totalPay * dicountFactors[i]);
                netPresentValue += value;
                double forDuration = value * distance;
                durationInYears += forDuration;
            }
            durationInYears /= netPresentValue;
        }
    }
}