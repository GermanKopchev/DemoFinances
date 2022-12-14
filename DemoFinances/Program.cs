using DemoFinances;

internal class Program
{
    private static void Main(string[] args)
    {
        DateTime startDate = new DateTime(2016,3,2);
        DateTime endDate = new DateTime(2019,9,2);
        int interest = 12;
        long nominal = 100000;
        double interestRate = 0.0445;


        Bond bond = new Bond(startDate, endDate, interest, nominal, interestRate, BondType.Regular);
        Calculate calculation = new Calculate();
        calculation.calculateBond(bond);
    }
}