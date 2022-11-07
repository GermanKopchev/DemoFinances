using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoFinances
{
    internal class Calculate
    {

        public List<DateTime> datesList(DateTime startDate, DateTime endDate, int interest)
        {
            List<DateTime> dates = new List<DateTime>();
            while (startDate < endDate)
            {
                dates.Insert(0, endDate);
                endDate = endDate.AddMonths(-interest).Date;
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

        //dt Period
        public List<double> periodLenght(List<DateTime> dates)
        {
            List<double> list = new List<double>();
            list.Add(0);
            for(int i = 0; i < dates.Count-1; i++)
            {
                list.Add(Days360(dates[i], dates[i + 1], true) / 360);
            }
            return list;
        }

        public List<double> distance(DateTime startDate, List<DateTime> dates)
        {
            List<double> list = new List<double>();
            foreach(DateTime date in dates)
            {
                if(date <= startDate) {
                    continue;
                }
                list.Add(Days360(startDate, date, true));
            }
            return list;
        }

        public List<int> redemption(List<DateTime> dates, int nominal)
        {
            List<int> list = new List<int>();
            for(int i = 0; i < dates.Count-1; i++)
            {
                list.Add(0);
            }
            list.Add(nominal);
            return list;
        }

        public List<int> capital(List<int> redemption, int nominal)
        {
            List<int> capitals = new List<int>();
            capitals.Add(nominal);
            for(int i = 1; i < redemption.Count; i++)
            {
                nominal -= redemption[i - 1];
                capitals.Add(nominal);
            }
            capitals.Add(nominal);
            return capitals;
        }

        public List<double> interestRatePayments(double interestRate, List<double> period, List<int> capital)
        {
            List<double> list = new List<double>();
            for(int i = 0; i < capital.Count; i++)
            {
                list.Add(interestRate * period[i] * capital[i]);
            }
            return list;
        }

        public List<double> totalPayment(List<int> redemtions, List<double> interestRatePyment)
        {
            List<double> list = new List<double>();
            for(int i = 0; i < redemtions.Count; i++)
            {
                list.Add(redemtions[i] + interestRatePyment[i]);
            }
            return list;
        }


        public void calculateBond(Bond bond)
        {


        }

        public void calculateFloaterBond(Bond bond)
        {

        }
    }
}
