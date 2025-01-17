using System;
using System.ComponentModel.Design;

namespace sl2a_pong;

class Program
{
    static async Task Main()
    {
        try
        {
            bool exit = false;

            //define the main thread in this application
            //other thrads in this application will be worker threads from Threadpool via Task.Run
            Thread mainThread = Thread.CurrentThread;

            //empty the whole console
            Console.Clear();

            //define and initialise variables & classes
            Menu menu = new();
            PongHandler pong = new();
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    //if this case is called execute the playpong method
                    await pong.PlayPong();
                    break;
                case "2":
                    //if this case is called execute the dislay the curent date and time on the screen
                    menu.DisplayDateTime();

                    break;
                case "3":
                    //if this case is called stop the console app and close
                    exit = true;

                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            //if the game is nolonger running, display a message and wait for keyboard input
            if (!exit)
            {
                Console.WriteLine("Press any key to return to menu");
                Console.ReadKey();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
        }
    }
}
