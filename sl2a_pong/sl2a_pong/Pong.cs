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
    private int leftRacketY1, leftRacketY2, leftRacketY3, rightRacketY1, rightRacketY2, rightRacketY3;
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

        leftRacketY1 = 0;
        leftRacketY2 = 0;
        leftRacketY3 = 0;
        rightRacketY1 = 0;
        rightRacketY2 = 0;
        rightRacketY3 = 0;
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

        //Execute the method to handle the movement of the ball
        pongBall.MoveBallObject();

        //keep the console open until the Escape key is held down
        while (true)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape)
                {
                    break;
                }
            }

            // Add a small delay to prevent high CPU usage
            await Task.Delay(100);
        }
        while (runTime == true) 
        { 
            
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
            //empty the console
            Console.Clear();

            Print("Right player won!");
            runTime = false;
            
        }
        else if (leftPlayerPoints > 10)
        {
            //empty the console
            Console.Clear();

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
    public void SetRacketPositions(int y1, int y2, int y3, int indicator)
    {
        if(indicator == 1)
        {
            leftRacketY1 = y1;
            leftRacketY2 = y2;
            leftRacketY3 = y3;
        }
        else if (indicator == 0)
        {
            rightRacketY1 = y1;
            rightRacketY2 = y2;
            rightRacketY3 = y3;
        }
    }
    public int GetLeftRacketY1()
    {
        return this.leftRacketY1;
    }
    public int GetLeftRacketY2()
    {
        return this.leftRacketY2;
    }
    public int GetLeftRacketY3()
    {
        return this.leftRacketY3;
    }
    public int GetRightRacketY1()
    {
        return this.rightRacketY1;
    }
    public int GetRightRacketY2()
    {
        return this.rightRacketY2;
    }
    public int GetRightRacketY3()
    {
        return this.rightRacketY3;
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