using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sl2a_pong
{
   public class Ball : PongHandler
   {
        //declare variables/properties
        private int leftRacketHeight;
        private int rightRacketHeight;
        private int ballX;
        private int ballY;
        private int racketLength;

        private bool isBallGoingDown;
        private bool isBallGoingRight;
        private string ballTile;

        public Ball() 
        {
            //initialised variables/properties
            leftRacketHeight = 0;
            rightRacketHeight = 0;
            racketLength = GetFieldWidth() / 4;

            ballX = GetFieldLength() / 2;
            ballY = GetFieldWidth() / 2;
            ballTile = "O";
            isBallGoingDown = true;
            isBallGoingRight = true;
        }
        public void MoveBallObject() 
        {
            while (GetRuntime() == true)
            {
                    while (!Console.KeyAvailable)
                    {
                        ThreadPool.QueueUserWorkItem((o) =>
                        {
                            //set the position of the cursor
                            //print the ball at the position of the cursor
                            Print(ballTile, ballX, ballY);

                            //the sleep function sets the time between 2 print operations
                            //if the sleep function is not used the ball will basicly play the game
                            //at a speed too high for humans to interact with.
                            Thread.Sleep(500);

                            //print empty space
                            Print(" ", ballX, ballY);
                        });

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
                                SetRightPlayerScore(GetRightPlayerScore() + 1);
                                ballY = GetFieldWidth() / 2;
                                ballX = GetFieldLength() / 2;
                                //Console.SetCursorPosition(GetScoreboardX(), GetScoreboardY());
                                //Console.WriteLine($"{leftPlayerPoints} | {rightPlayerPoints}");
                                Print($"{GetLeftPlayerScore()} | {GetRightPlayerScore()}", GetScoreboardX(), GetScoreboardY());

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
                                SetLeftPlayerScore(GetLeftPlayerScore() + 1);
                                ballY = GetFieldWidth() / 2;
                                ballX = GetFieldLength() / 2;
                                //Console.SetCursorPosition(GetScoreboardX(), GetScoreboardY());
                                //Console.WriteLine($"{leftPlayerPoints} | {rightPlayerPoints}");
                                Print($"{GetLeftPlayerScore()} | {GetRightPlayerScore()}", GetScoreboardX(), GetScoreboardY());

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
}