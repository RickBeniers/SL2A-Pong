using System;
using System.ComponentModel.Design;

namespace sl2a_pong;

class Program
{
    static void Main()
    {
        //define the main thread in this application
        Thread mainThread = Thread.CurrentThread;

        //empty the whole console
        Console.Clear();

        //define and initialise variables & classes
        Menu menu = new();
        PongHandler pong = new();
        bool exit = false;

        string input = Console.ReadLine();
        switch (input)
        {
           case "1":
                //if this case is called execute the playpong method
                pong.PlayPong();
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

        if (!exit)
        {
            //if no input has been given yet than display this message
           Console.WriteLine("\n Press any key to return to the main menu...");
           Console.ReadKey();
        }  
    }
}
