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

        static decimal GetCoin(decimal Coin)
        {
            int input = 0;
            bool tryparse = false;
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
                            if (Coin >= 0m)
                            {
                                Console.Clear();
                                Print($"Voici votre remboursement", 0,0,0, Coin, "A la prochaine fois !");
                                Console.Write("Voulez-vous quittez ? [O/N]");
                                char Quit = Console.ReadLine()[0];
                                if (Quit == 'n' || Quit == 'N')
                                {
                                    Console.Clear();
                                    Main();
                                }
                                else if (Quit == 'o' || Quit == 'O')
                                {
                                
                                    Environment.Exit(0);
                                }
                                else
                                {
                                    Console.WriteLine("Veuillez entrez un charactere valide.");
                                    Thread.Sleep(1000);
                                    Main();
                                }
                            }
                            
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
                Console.Clear();
                input = GetSelection(input);

                if (candies[GetCandy(input)].Stock == 0)
                {
                    // should add color
                    Console.Clear();
                    Print($"{candies[GetCandy(input)].Name} est vide", input);
                    Console.WriteLine($"{candies[GetCandy(input)].Name} est vide, veuillez faire un autre choix.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    // Main();
                }
                else if (candies[GetCandy(input)].Stock > 0)
                {
                    // should add color
                    do
                    {
                        Console.Clear();
                        Print($"{candies[GetCandy(input)].Name}", input,candies[GetCandy(input)].Price,Coin);
                        Coin = GetCoin(Coin);
                        costReturn = Coin - candies[GetCandy(input)].Price;
                        Console.Clear();
                        if (Coin >= candies[GetCandy(input)].Price)
                        {   
                                Console.Clear();
                                candies[GetCandy(input)].Stock--;
                                Print($"Prenez votre friandise", input,candies[GetCandy(input)].Price,Coin, costReturn, candies[GetCandy(input)].Name);
                                Coin = 0;
                                Thread.Sleep(1500);
                                Console.Clear();
                                // Main();
                        }

                    } while (candies[GetCandy(input)].Price >= Coin);
                }
            }
        }
    }

}