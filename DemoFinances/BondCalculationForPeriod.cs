namespace DemoFinances
{
    internal class BondCalculationForPeriod
    {
        private DateTime date;
        private double interestRate;
        private double redemption;
        private double capital;
        private double dTPeriod;
        private double interestRatePayment;
        private double totalPayment;
        private double distance;
        private double discountFactors;
        private double value;

        public BondCalculationForPeriod(DateTime date, double interestRate, double redemption, double capital, double dTPeriod, double interestRatePayment, double totalPayment, double distance, double discountFactors, double value)
        {
            this.date = date;
            this.interestRate = interestRate;
            this.redemption = redemption;
            this.capital = capital;
            this.dTPeriod = dTPeriod;
            this.interestRatePayment = interestRatePayment;
            this.totalPayment = totalPayment;
            this.distance = distance;
            this.discountFactors = discountFactors;
            this.value = value;
        }

        public DateTime Date { get => date; set => date = value; }
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
