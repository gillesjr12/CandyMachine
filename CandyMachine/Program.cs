using Project;
using static Project.Board;
using static Project.Candy;
using static Project.Data;
using System.Globalization;
using System.Threading;


namespace CandyMachine
{
    
    class Program
    {
        
        static void Main()
        {

            int output = 0;
            bool tryparse = false;
            Data dataCandy = new Data();
            Candy[] candies = dataCandy.LoadCandies();
            Console.WriteLine($"{candies[1].Name}");

        }
    }
}