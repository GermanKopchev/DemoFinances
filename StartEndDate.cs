using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoFinances
{
    internal class StartEndDate
    {
        public static DateTime startEndDate()
        {
            Console.WriteLine("Enter day:");
            int day = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter month:");
            int month = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter year:");
            int year = Convert.ToInt32(Console.ReadLine());
            DateTime date = new DateTime(year, month, day);
            return date;
        }
    }
}
