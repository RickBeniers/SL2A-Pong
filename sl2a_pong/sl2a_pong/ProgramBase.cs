using System;
namespace sl2a_pong;

public class ProgramBase
{
    public ProgramBase()
    {
        //the class constructor
    }

    //a method that can be derived from to print strings to the console
    public virtual void Print(string message)
    {
        Console.WriteLine(message);
    }
    public virtual void Print()
    {
        Console.WriteLine("message");
    }
}
