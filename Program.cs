using DemoFinances;

internal class Program
{
    private static void Main(string[] args)
    {
        /*        Console.WriteLine("Enter start date:");
                DateTime startDate = StartEndDate.startEndDate();

                Console.WriteLine("Enter end date:");
                DateTime endDate = StartEndDate.startEndDate();

                Console.WriteLine("Enter interest rate period in months:");
                int interest = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter nominal:");
                long nominal = Convert.ToInt64(Console.ReadLine());

                Console.WriteLine("Enter interest rate:");
                double interestRate = Convert.ToDouble(Console.ReadLine());*/

        DateTime startDate = new DateTime(2016,3,2);
        DateTime endDate = new DateTime(2019,9,2);
        int interest = 12;
        long nominal = 100000;
        double interestRate = 4.45;


        Bond bond = new Bond(startDate, endDate, interest, nominal, interestRate);

        List<DateTime> dates = new List<DateTime>();
        while (startDate < endDate)
        {
            dates.Insert(0, endDate);
            endDate = endDate.AddMonths(-interest).Date;
        }
        dates.Insert(0, startDate);
        foreach (DateTime date in dates)
        {
            Console.WriteLine(date);
        }
    }
}