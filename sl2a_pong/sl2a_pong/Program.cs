using System;
using System.ComponentModel.Design;

namespace sl2a_pong;

class Program : ProgramBase
{
    static void Main(string[] args)
    {
        bool exit = false;

        while (!exit)
        {
            try 
            { 
                Console.Clear();

                //define and initialise classes
                //Menu menu = new Menu();
                //PongHandler pong = new PongHandler();

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        PongHandler pong = new PongHandler();
                        pong.PlayPong();
                        break;
                    case "2":
                        Menu menu = new Menu();
                        menu.DisplayDateTime();
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        //this.Print("Invalid option. Please try again.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\n Press any key to return to the main menu...");
                    //this.Print("\nPress any key to return to the main menu...");
                    Console.ReadKey();
                }
            }
            catch (IOException) 
            { 
            
            }
        }
    }
}
