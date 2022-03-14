using Project;
using static Project.Board;
using static Project.Candy;
using static Project.Data;
using System.Globalization;
using System.Reflection.Emit;
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

        static decimal GetCoin(int input)
        {
            input = 0;
            bool tryparse = false;
            decimal Coin = 0;
            Console.WriteLine("[0] = Annuler");
            Console.WriteLine("[1] = 5c");
            Console.WriteLine("[2] = 10c");
            Console.WriteLine("[3] = 25c");
            Console.WriteLine("[4] = 1$");
            Console.WriteLine("[5] = 2$");
            Console.Write("--> ");
            do
            {
                tryparse = int.TryParse(Console.ReadLine(), out input);
                if (input <= 5 || input >= 0)
                {
                    switch (input)
                    {
                        case 0:
                            Environment.Exit(0);
                            break;
                        case 1:
                            Coin += 0.05m;
                            break;
                        case 2:
                            Coin += 0.10m;
                            break;
                        case 3:
                            Coin += 0.25m;
                            break;
                        case 4:
                            Coin += 1m;
                            break;
                        case 5:
                            Coin += 2m;
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
            decimal Coin = 0;
            decimal costReturn;
            bool IsRunning = true;

            while (IsRunning == true)
            {
                input = GetSelection(input);

                if (candies[GetCandy(input)].Stock == 0)
                {
                    // should add color
                    Print($"{candies[GetCandy(input)].Name} est vide", input);
                }
                else if (candies[GetCandy(input)].Stock > 0)
                {
                    // should add color
                    Console.Clear();
                    do
                    {
                        Print($"{candies[GetCandy(input)].Name}", input,candies[GetCandy(input)].Price,Coin);
                        Coin += GetCoin(input);
                        costReturn = Coin - candies[GetCandy(input)].Price;
                        Console.Clear();
                        if (Coin > candies[GetCandy(input)].Price)
                        {
                            Console.Clear();
                            Print($"{candies[GetCandy(input)].Name}", input,candies[GetCandy(input)].Price,Coin, costReturn);
                        }
                    } while (candies[GetCandy(input)].Price >= Coin);

                    if (candies[GetCandy(input)].Price <= Coin)
                    {
                        Print($"Prenez votre friandise", input,candies[GetCandy(input)].Price,Coin, costReturn, $"{candies[GetCandy(input)].Name}");
                    
                    }
                }
            

            }
        }
            
        
    }

}