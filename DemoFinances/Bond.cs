namespace DemoFinances
{
    internal class Bond
    {
        private DateTime startDate;
        private DateTime endDate;
        int interestPeriod;
        long nominal;
        double interestRate;

        public Bond(DateTime startDate, DateTime endDate, int interest, long nominal, double interestRate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            this.interestPeriod = interest;
            this.nominal = nominal;
            this.interestRate = interestRate;
        }

        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public int InterestPeriod { get => interestPeriod; set => interestPeriod = value; }
        public long Nominal { get => nominal; set => nominal = value; }
        public double InterestRate { get => interestRate; set => interestRate = value; }
    }
}
