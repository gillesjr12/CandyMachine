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

        private static int GetSelection(int input) // function for the selection's user
        {
            bool tryParse = false;

            do
            {
                    Print();
                    Console.Write("Veuillez faire votre selection [1-25] : ");
                    tryParse = int.TryParse(Console.ReadLine(), out input); // Users are forced to use a number
                    if (input < 0 || input > 25) 
                    { 
                             Console.WriteLine("Veuillez entrez un nombre valide.");
                             Thread.Sleep(1000); 
                    }
        
            } while (input < 0 || input > 25 || tryParse != true);

            return input;
        }


        public static int GetCandy(int input)
        {
            return input - 1;
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
                if (input < 5 || input > 0)
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
        

        static void Main()
        {
            int input = 0;
            Data dataCandy = new Data();
            Candy[] candies = dataCandy.LoadCandies();
            
            // byte[] ArrayCandy = new byte[0];
            // double Coin = 0;

            // do
            // {
                input = GetSelection(input);
            // GetCandy(ArrayCandy, input);
            // GetCoin(Coin);
            if (candies[GetCandy(input)].Stock == 0)
            {
                Print($"{candies[GetCandy(input)].Name} est vite");
            }
                
            // Console.WriteLine(GetSelection(input));


            // }

        }
        
    }

}