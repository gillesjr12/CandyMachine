using Project;
using static Project.Board;
using static Project.Candy;
using static Project.Data;
using System.Globalization;
using System.Threading;
using System.Transactions;


namespace CandyMachine
{

    class Program
    {

        static byte GetSelection(byte input) // function for the selection's user
        {
            input = 0;
            string message = "Votre choix";
            bool tryParse = false;
            do
            {
                do
                {
                    Print();
                    Console.Write("Veuillez faire votre selection [1-25] : ");
                    tryParse = byte.TryParse(Console.ReadLine(), out input); // Users are forced to use a number
                    if (input < 0 || input > 25)
                    {
                        Console.WriteLine("Veuillez entrez un nombre valide.");
                        Thread.Sleep(1000);
                    }
                    else if (input > 0 || input < 25)
                    {
                    }
                } while (input < 0 || input > 25);
            } while (tryParse != true); // If the user doesn't enter a number, we loop

            return input;
        }

        static byte GetCandy(byte[] candies)
        {
            candies[24] = byte.Parse(Console.ReadLine());
            return candies[-1];
        }

        static double GetCoin(double Coin)
        {
            byte input = 0;
            bool tryparse = false;
            Coin = 0;
            Console.WriteLine("[0] = Annuler");
            Console.WriteLine("[1] = 5c");
            Console.WriteLine("[2] = 10c");
            Console.WriteLine("[3] = 25c");
            Console.WriteLine("[4] = 1$");
            Console.WriteLine("[5] = 2$");
            Console.Write("--> ");
            do
            {
                tryparse = byte.TryParse(Console.ReadLine(), out input);
                if (input < 5 || input >= 5)
                {
                    switch (input)
                    {
                        case 0:
                            return 0;
                        case 1:
                            Coin += 0.05d;
                            break;
                        case 2:
                            Coin += 0.10d;
                            break;
                        case 3:
                            Coin += 0.25d;
                            break;
                        case 4:
                            Coin += 1d;
                            break;
                        case 5:
                            Coin += 2d;
                            break;
                    }
                }
            } while (tryparse != true);
            
            return Coin;
        }

        static Candy[] LoadCandies()
        {
            Data dataCandy = new Data();
            Candy[] candies = dataCandy.LoadCandies();
            return candies;
        }

        static void Main()
        {

            byte input = 0;
            byte[] ArrayCandy = new byte[0];
            double Coin = 0;
            bool tryparse = false;
            Data dataCandy = new Data();
            Candy[] candies = dataCandy.LoadCandies();
            GetSelection(input);
            GetCandy(ArrayCandy);
            if (candies[GetSelection(input)].Stock == 0)
            {
                Print($"{candies[0].Name}",0,0,0,0,"");
            }
            GetCoin(Coin);

        }
        
    }

}