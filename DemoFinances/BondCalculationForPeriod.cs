namespace DemoFinances
{
    internal class BondCalculationForPeriod
    {
        private DateTime startDate;
        private DateTime endDate;
        private double interestRate;
        private double redemption;
        private double capital;
        private double dTPeriod;
        private double interestRatePayment;
        private double totalPayment;
        private double distance;
        private double discountFactors;
        private double value;

        public BondCalculationForPeriod(DateTime startDate, DateTime endDate, double interestRate, double redemption, double capital, double dTPeriod)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            this.interestRate = interestRate;
            this.redemption = redemption;
            this.capital = capital;
            this.dTPeriod = dTPeriod;
        }

        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public double InterestRate { get => interestRate; set => interestRate = value; }
        public double Redemption { get => redemption; set => redemption = value; }
        public double Capital { get => capital; set => capital = value; }
        public double DTPeriod { get => dTPeriod; set => dTPeriod = value; }
        public double InterestRatePayment { get => interestRatePayment; set => interestRatePayment = value; }
        public double TotalPayment { get => totalPayment; set => totalPayment = value; }
        public double Distance { get => distance; set => distance = value; }
        public double DiscountFactors { get => discountFactors; set => discountFactors = value; }
        public double Value { get => value; set => this.value = value; }
    }
}
