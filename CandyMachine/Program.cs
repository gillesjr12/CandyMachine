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

        static byte GetSelection(byte input)
        {
            input = 0;
            string message = "Votre choix";
            bool tryParse = false;
            Print(PadBoth(message,input));
            do
            {
                Console.Write("Veuillez faire votre selection [1-25] : ");
                tryParse = byte.TryParse(Console.ReadLine(), out input);
                if (input<0 || input>25)
                {
                    Console.WriteLine("Veuillez entrez un nombre valide.");
                }
                else if (input>0 || input <25)
                {
                    Print(PadBoth(message,input));
                }
            } while (tryParse != true);

            return input;
        }

        static void Main()
        {

            byte output = 0;
            bool tryparse = false;
            Data dataCandy = new Data();
            Candy[] candies = dataCandy.LoadCandies();
            Console.WriteLine($"{candies[1].Name}");
            GetSelection(output);

        }
        
    }

}