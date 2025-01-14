using System;
using System.Linq;
using System.Security.AccessControl;

namespace sl2a_pong;

public class PongHandler : ProgramBase 
{
    //define variables
    private string fieldTile;
    private string line;

    private int fieldLength, fieldWidth;

    private int leftPlayerPoints;
    private int rightPlayerPoints;
    private int scoreboardX;
    private int scoreboardY;

    private bool runTime;

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

    public void PlayPong()
    {
        Console.Clear();

        //initialise the racket and ball classes
        Racket leftRacket = new Racket(1);
        Racket rightRacket = new Racket(0);
        Ball pongBall = new Ball();

        while (runTime == true)
        {
            //hide the cursor
            Console.CursorVisible = false;
            //move the cursor to the top left corner and print a "line"
            Console.SetCursorPosition(0, 0);
            Print(line);
            //move the cursor to another position and print another "line"
            Console.SetCursorPosition(0, fieldWidth);
            Print(line);

            

            //During runtime, tell the rackets to listen for keyboardinputs and move accordingly.
            leftRacket.MoveRacket();
            rightRacket.MoveRacket();

            for (int i = 1; i < fieldWidth; i++)
            {
                //print empty space betwee the two walls.
                Console.SetCursorPosition(0, i);
                //Console.WriteLine(" ");
                Print(" ");
                Console.SetCursorPosition(fieldLength - 1, i);
                //Console.WriteLine(" ");
                Print(" ");
            }
        }
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
            //Console.WriteLine("Right player won!");
            Print("Right player won!");
            runTime = false;
        }
        else if (leftPlayerPoints > 10)
        {
            //Console.WriteLine("Left player won!");
            Print("Left player won!");
            runTime = false;
        }
    }
    //functions to share the fieldwidth and fieldlength with other classes (seters and getters)
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
    public override void Print(string message, int X, int Y)
    {
        Console.SetCursorPosition(X, Y);
        Console.WriteLine(message);
    }
}