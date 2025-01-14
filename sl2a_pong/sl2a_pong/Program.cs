using System;
using System.ComponentModel.Design;

namespace sl2a_pong;

class Program
{
    static void Main()
    {
        //empty the whole console
        Console.Clear();

        //define and initialise variables & classes
        Menu menu = new();
        PongHandler pong = new();
        bool exit = false;

        //define the main thread in this application
        Thread mainThread = Thread.CurrentThread;

        string input = Console.ReadLine();
        switch (input)
        {
           case "1":           
           pong.PlayPong();

           break;
           case "2":
           menu.DisplayDateTime();

           break;
           case "3":
           exit = true;

           break;
           default:
           Console.WriteLine("Invalid option. Please try again.");
           break;
        }

        if (!exit)
        {
           Console.WriteLine("\n Press any key to return to the main menu...");
           Console.ReadKey();
        }  
    }
}
