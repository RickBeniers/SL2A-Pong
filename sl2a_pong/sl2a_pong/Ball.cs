using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sl2a_pong
{
   public class Ball : PongHandler
   {
        private int racketLength;
        private int leftRacketHeight;
        private int rightRacketHeight;
        private int ballX;
        private int ballY;
        private bool isBallGoingDown;
        private bool isBallGoingRight;
        private string ballTile;

        public Ball() 
        {
            leftRacketHeight = 0;
            rightRacketHeight = 0;

            ballX = GetFieldLength() / 2;
            ballY = GetFieldWidth() / 2;
            ballTile = "O";
            isBallGoingDown = true;
            isBallGoingRight = true;

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

                if (ballY == 1 || ballY == GetFieldWidth() - 1)
                {
                    isBallGoingDown = !isBallGoingDown;
                }

                if (ballX == 1)
                {
                    if (ballY >= leftRacketHeight + 1 && ballY <= leftRacketHeight + racketLength)
                    {
                        isBallGoingRight = !isBallGoingRight;
                    }
                    else
                    {
                        //rightPlayerPoints++;
                        SetRightPlayerScore(GetRightPlayerScore()+1);
                        ballY = GetFieldWidth() / 2;
                        ballX = GetFieldLength() / 2;
                        Console.SetCursorPosition(GetScoreboardX(), GetScoreboardY());
                        //Console.WriteLine($"{leftPlayerPoints} | {rightPlayerPoints}");
                        Print($"{GetLeftPlayerScore()} | {GetRightPlayerScore()}");

                        if (GetRightPlayerScore() > 10)
                        {
                            DisplayWinner();
                        }
                    }
                }
                if (ballX == GetFieldLength() - 2)
                {
                    if (ballY >= rightRacketHeight + 1 && ballY <= rightRacketHeight + racketLength)
                    {
                        isBallGoingRight = !isBallGoingRight;
                    }
                    else
                    {
                        //leftPlayerPoints++;
                        SetLeftPlayerScore(GetLeftPlayerScore()+1);
                        ballY = GetFieldWidth() / 2;
                        ballX = GetFieldLength() / 2;
                        Console.SetCursorPosition(GetScoreboardX(), GetScoreboardY());
                        //Console.WriteLine($"{leftPlayerPoints} | {rightPlayerPoints}");
                        Print($"{GetLeftPlayerScore()} | {GetRightPlayerScore()}");

                        if (GetLeftPlayerScore() > 10)
                        {
                            DisplayWinner();
                        }
                    }
                }
            }
        }
   }
}