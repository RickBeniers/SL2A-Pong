using System;
using System.Linq;
using System.Security.AccessControl;

namespace sl2a_pong;

public class PongHandler : ProgramBase
{
    //declare variables
    private string fieldTile;
    private string line;

    private int fieldLength, fieldWidth;

    private int leftPlayerPoints;
    private int rightPlayerPoints;
    private int scoreboardX;
    private int scoreboardY;

    private bool runTime;

    //declare a lock object which only 1 thread can acces at a time
    private static object threadLock = new object();

    public PongHandler()
    {
        //Consructor to initialise variables
        fieldLength = 50;
        fieldWidth = 15;
        fieldTile = "#";

        leftPlayerPoints = 0;
        rightPlayerPoints = 0;
        scoreboardX = fieldLength / 2 - 2;
        scoreboardY = fieldWidth + 3;
        runTime = true;
        line = string.Concat(Enumerable.Repeat(fieldTile, fieldLength));
    }

    public async Task PlayPong()
    {
        //empty the console
        Console.Clear();

        //hide the cursor
        Console.CursorVisible = false;

        //move the cursor to the top left corner and print a "line"
        //This method is executed asynchronously by use of a task
        await Print(line, 0, 0);

        //move the cursor to another position and print another "line"
        await Print(line, 0, fieldWidth);

        for (int i = 1; i < fieldWidth; i++)
        {
            //print empty space betwee the two walls of lines.
            await Print(" ", 0, i);
            await Print(" ", fieldLength - 1, i);
        }

        //initialise the racket classes and the ball class
        Racket leftRacket = new Racket(1);
        Racket rightRacket = new Racket(0);
        Ball pongBall = new Ball();

        //execute the method that checks for keyboard input and changes the racket heights
        //Execute the print racket method.
        leftRacket.MoveRacket();
        leftRacket.PrintRacket();
        //execute the method that checks for keyboard input and changes the racket heights
        //Execute the print racket method.
        rightRacket.MoveRacket();
        rightRacket.PrintRacket();
        //Execute the method to handle the movement of the ball
        pongBall.MoveBallObject();

        //keep the console open untill Enter key is pressed
        Console.ReadLine();
    }
    public void DisplayWinner()
    {
        //empty the console
        Console.Clear();
        //place the cursor position to the top left corner
        Console.SetCursorPosition(0, 0);
        //display a message at the position of the cursor
        if (rightPlayerPoints > 10)
        {
            Print("Right player won!");
            runTime = false;
        }
        else if (leftPlayerPoints > 10)
        {
            Print("Left player won!");
            runTime = false;
        }
    }
    //Methods to share the fieldwidth and fieldlength with other classes (seters and getters)
    public int GetFieldWidth()
    {
        return this.fieldWidth;
    }
    public int GetFieldLength()
    {
        return this.fieldLength;
    }
    public bool GetRuntime()
    {
        return this.runTime;
    }
    public int GetScoreboardX()
    {
        return this.scoreboardX;
    }
    public int GetScoreboardY()
    {
        return this.scoreboardY;
    }
    public int GetLeftPlayerScore()
    {
        return this.leftPlayerPoints;
    }
    public int GetRightPlayerScore()
    {
        return this.rightPlayerPoints;
    }
    public void SetLeftPlayerScore(int score)
    {
        leftPlayerPoints = score;
    }
    public void SetRightPlayerScore(int score)
    {
        rightPlayerPoints = score;
    }

    // a method derived from ProgramBase to implement a method to print to the console
    public override void Print(string message)
    {
        Console.WriteLine(message);
    }
    public override async Task Print(string message, int X, int Y)
    {
        lock (threadLock)
        {
            Console.SetCursorPosition(X, Y);
            Console.WriteLine(message);
        }
    }
}