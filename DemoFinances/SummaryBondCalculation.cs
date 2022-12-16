using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoFinances
{
    internal class SummaryBondCalculation
    {
        private double value;
        private double cleanValue;
        private double cleanPrice;
        private double accruedInterest;
        private double dirtyValue;
        private double dirtyPrice;
        private double MarketCleanPrice;
        private double profit;

        public SummaryBondCalculation()
        {
        }

        public double Value { get => value; set => this.value = value; }
        public double CleanValue { get => cleanValue; set => cleanValue = value; }
        public double CleanPrice { get => cleanPrice; set => cleanPrice = value; }
        public double AccruedInterest { get => accruedInterest; set => accruedInterest = value; }
        public double DirtyValue { get => dirtyValue; set => dirtyValue = value; }
        public double DirtyPrice { get => dirtyPrice; set => dirtyPrice = value; }
        public double MarketCleanPrice1 { get => MarketCleanPrice; set => MarketCleanPrice = value; }
        public double Profit { get => profit; set => profit = value; }
    }
}
