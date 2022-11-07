namespace DemoFinances
{
    internal class Bond
    {
        private DateTime startDate;
        private DateTime endDate;
        int interest;
        long nominal;
        double interestRate;

        public Bond(DateTime startDate, DateTime endDate, int interest, long nominal, double interestRate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            this.interest = interest;
            this.nominal = nominal;
            this.interestRate = interestRate;
        }

        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public int Interest { get => interest; set => interest = value; }
        public long Nominal { get => nominal; set => nominal = value; }
        public double InterestRate { get => interestRate; set => interestRate = value; }
    }
}
