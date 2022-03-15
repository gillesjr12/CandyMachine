using System.Drawing;
using Project;
using static Project.Board;
using static Project.Candy;
using static Project.Data;
using System.Globalization;
using System.Reflection.Emit;
using System.Threading;
using System.Transactions;
using Microsoft.VisualBasic.CompilerServices;


namespace CandyMachine
{

    class Program
    {

        private static int userNumInput(int input)
        {
            bool tryParse = false;
            do
            {
                tryParse = int.TryParse(Console.ReadLine(), out input);
            } while (tryParse != true);
            return input;
        }
        
        
        private static int GetSelection(int input) // function for the selection's user
        {

            do
            {
                Print();
                Console.Write("Veuillez faire votre selection [1-25] : ");
                input = userNumInput(input);
                if (input < 0 || input > 25)
                {
                    Console.WriteLine("Veuillez entrez un nombre valide.");
                    Thread.Sleep(1000);
                }

            } while (input < 0 || input > 25);

            return input;
        }


        public static int GetCandy(int input)
        {
            return input - 1;
        }
        

        static decimal GetCoin(decimal Coin)
        {
            int input = 0;
            Console.WriteLine("[0] = Annuler");
            Console.WriteLine("[1] = 5c");
            Console.WriteLine("[2] = 10c");
            Console.WriteLine("[3] = 25c");
            Console.WriteLine("[4] = 1$");
            Console.WriteLine("[5] = 2$");
            Console.Write("--> ");
        
                input = userNumInput(input);
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
                return Coin;
        }

        static void Music()
        {
            Console.Write("R");Console.Beep(1188,500);Console.Write("é");Console.Beep(1408,250);Console.Write("g");Console.Beep(1760,500);Console.Write("a");Console.Beep(1584,250);Console.Write("l");
            Console.Beep(1408,250);Console.Write("e");Console.Beep(1320,750);Console.Write("z");Console.Beep(1056,250);Console.Write("-");Console.Beep(1320,500);Console.Write("v");
            Console.Beep(1188,250);Console.Write("o");Console.Beep(1056,250);Console.Write("u");Console.Beep(990,500);Console.Write("s");Console.Beep(990,250);Console.Write(" b");
            Console.Beep(1056,250);Console.Write("i");Console.Beep(1188,500);Console.Write("e");Console.Beep(1320,500);Console.Write("n");Console.Beep(1056,500);Console.Write(" !");
            Console.Beep(880,500);Console.Write("!");Console.Beep(880,500);
            Thread.Sleep(500);
        }

         static void videColor()
         {
             Console.ForegroundColor = ConsoleColor.Red;
         }
         static void normalColor()
         {
             Console.ForegroundColor = ConsoleColor.White;
         }
        
        static void Main()
        {
            int input = 0;
            Data dataCandy = new Data();
            Candy[] candies = dataCandy.LoadCandies();
            decimal Coin = 0;
            decimal costReturn;
            decimal tmp = 0;
            bool IsRunning = true;
            

            while (IsRunning == true)
            {

                Console.Clear();
                Print();
                input = GetSelection(input);

                if (candies[GetCandy(input)].Stock == 0)
                {
                    Console.Clear();
                    videColor();
                    Print($"{candies[GetCandy(input)].Name} est vide", input);
                    Console.WriteLine($"{candies[GetCandy(input)].Name} est vide, veuillez faire un autre choix.");
                    Thread.Sleep(3000);
                    normalColor();
                }
                else if (candies[GetCandy(input)].Stock > 0)
                {
                    do
                    {
                        Console.Clear();
                        Print($"{candies[GetCandy(input)].Name}", input,candies[GetCandy(input)].Price,Coin);
                        Coin = GetCoin(Coin);
                        costReturn = Coin - candies[GetCandy(input)].Price;
                        if (Coin >= candies[GetCandy(input)].Price )
                        {   
                                Console.Clear();
                                candies[GetCandy(input)].Stock--;
                                Print($"Prenez votre friandise", input,candies[GetCandy(input)].Price,Coin, costReturn, candies[GetCandy(input)].Name);
                                Console.WriteLine($"Vous avez acheté un {candies[GetCandy(input)].Name} à {candies[GetCandy(input)].Price}$ !");
                                Music();
                                // Console.WriteLine("Régalez-vous bien !");
                                Thread.Sleep(2000);
                        }

                    } while (candies[GetCandy(input)].Price > Coin);
                    Coin = 0;
                }
            }
        }
    }
}