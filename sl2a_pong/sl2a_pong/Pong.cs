using System;
using System.Linq;
using System.Security.AccessControl;

namespace sl2a_pong;

public class PongHandler : ProgramBase 
{
    //define variables
    private string fieldTile;
    private string line;
    private string racketTile;
    private string ballTile;

    private int fieldLength, fieldWidth;
    private int racketLength;
    private int leftRacketHeight;
    private int rightRacketHeight;
    private int ballX;
    private int ballY;

    private int leftPlayerPoints;
    private int rightPlayerPoints;
    private int scoreboardX;
    private int scoreboardY;

    private bool isBallGoingDown;
    private bool isBallGoingRight;
    private bool runTime;

    private Racket leftRacket;
    private Racket rightRacket;

    public PongHandler()
    {
        //Consructor to initialise variables
        fieldLength = 50;
        fieldWidth = 15;
        fieldTile = "#";
        

        racketLength = fieldWidth / 4;
        racketTile = "|";
        leftRacketHeight = 0;
        rightRacketHeight = 0;

        ballX = fieldLength / 2;
        ballY = fieldWidth / 2;
        ballTile = "O";
        isBallGoingDown = true;
        isBallGoingRight = true;

        leftPlayerPoints = 0;
        rightPlayerPoints = 0;
        scoreboardX = fieldLength / 2 - 2;
        scoreboardY = fieldWidth + 3;
        runTime = true;
        line = string.Concat(Enumerable.Repeat(fieldTile, fieldLength));

        //initialise the racket and ball classes
        leftRacket = new Racket(1);
        leftRacket.PrintRacket();
        rightRacket = new Racket(0);
        rightRacket.PrintRacket();
    }

    public void PlayPong()
    {
        Console.Clear();

        while (runTime == true)
        {
            //hide the cursor
            Console.CursorVisible = false;
            //move the cursor to the top left corner and print a "line"
            Console.SetCursorPosition(0, 0);
            //Console.WriteLine(line);
            Print(line);
            //move the cursor to another position and print another "line"
            Console.SetCursorPosition(0, fieldWidth);
            //Console.WriteLine(line);
            Print(line);

            //for (int i = 0; i < racketLength; i++)
            //{
            //    //print the racket that the player will use to deflect the ball
            //    Console.SetCursorPosition(0, i + 1 + leftRacketHeight);
            //    //Console.WriteLine(racketTile);
            //    Print(racketTile);
            //    Console.SetCursorPosition(fieldLength - 1, i + 1 + rightRacketHeight);
            //    //Console.WriteLine(racketTile);
            //    Print(racketTile);
            //}

            while (!Console.KeyAvailable)
            {
                //set the position of the cursor
                Console.SetCursorPosition(ballX, ballY);
                //print the ball at the position of the cursor
                Print(ballTile);
                //the sleep function sets the time between 2 print operations
                //if the sleep function is not used the ball will basicly play the game
                //at a speed too high for humans to interact with.
                Thread.Sleep(100);

                Console.SetCursorPosition(ballX, ballY);
                Print(" ");

                if (isBallGoingDown)
                {
                    ballY++;
                }
                else
                {
                    ballY--;
                }
                if (isBallGoingRight)
                {
                    ballX++;
                }
                else
                {
                    ballX--;
                }

                if (ballY == 1 || ballY == fieldWidth - 1)
                {
                    isBallGoingDown = !isBallGoingDown;
                }

                if (ballX == 1)
                {
                    if (ballY >= leftRacket.GetLeftRacketHeight() + 1 && ballY <= leftRacket.GetLeftRacketHeight() + racketLength)
                    {
                        isBallGoingRight = !isBallGoingRight;
                    }
                    else
                    {
                        rightPlayerPoints++;
                        ballY = fieldWidth / 2;
                        ballX = fieldLength / 2;
                        Console.SetCursorPosition(scoreboardX, scoreboardY);
                        //Console.WriteLine($"{leftPlayerPoints} | {rightPlayerPoints}");
                        Print($"{leftPlayerPoints} | {rightPlayerPoints}");

                        if (rightPlayerPoints > 10)
                        {
                            DisplayWinner();
                        }
                    }
                }
                if (ballX == fieldLength - 2)
                {
                    if (ballY >= rightRacket.GetRightRacketHeight() + 1 && ballY <= rightRacket.GetRightRacketHeight() + racketLength)
                    {
                        isBallGoingRight = !isBallGoingRight;
                    }
                    else
                    {
                        leftPlayerPoints++;
                        ballY = fieldWidth / 2;
                        ballX = fieldLength / 2;
                        Console.SetCursorPosition(scoreboardX, scoreboardY);
                        //Console.WriteLine($"{leftPlayerPoints} | {rightPlayerPoints}");
                        Print($"{leftPlayerPoints} | {rightPlayerPoints}");

                        if (leftPlayerPoints > 10)
                        {
                            DisplayWinner();
                        }
                    }
                }
            }

            //switch (Console.ReadKey().Key)
            //{
            //    //if one of the players presses the up, down, W or S keys than
            //    //change the corresponding height of the racket
            //    case ConsoleKey.UpArrow:
            //        if (rightRacketHeight > 0)
            //        {
            //            rightRacketHeight--;
            //        }
            //        break;
            //    case ConsoleKey.DownArrow:
            //        if (rightRacketHeight < fieldWidth - racketLength - 1)
            //        {
            //            rightRacketHeight++;
            //        }
            //        break;
            //    case ConsoleKey.W:
            //        if (leftRacketHeight > 0)
            //        {
            //            leftRacketHeight--;
            //        }
            //        break;
            //    case ConsoleKey.S:
            //        if (leftRacketHeight < fieldWidth - racketLength - 1)
            //        {
            //            leftRacketHeight++;
            //        }
            //        break;
            //}
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
        //when the game is nolonger running
        //stop the ball from moving
        while (runTime == false)
        {
            ballX = 0;
            ballY = 0;
            isBallGoingDown = false;
            isBallGoingRight = false;
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
    //functions to share the fieldwidth and fieldlength with other classes
    public int GetFieldWidth() 
    {
        return this.fieldWidth;
    }
    public int GetFieldLength() 
    {
        return this.fieldLength;
    }

    // a method derived from ProgramBase to implement a method to print to the console
    public override void Print(string message)
    {
        Console.WriteLine(message);
    }
}