namespace sl2a_pong
{
    public class Ball : PongHandler
    {
        //declare variables/properties
        private int ballX;
        private int ballY;
        private bool isBallGoingDown;
        private bool isBallGoingRight;
        private string ballTile;

        public Ball()
        {
            //initialised variables/properties
            ballX = GetFieldLength() / 2;
            ballY = GetFieldWidth() / 2;
            ballTile = "O";
            isBallGoingDown = true;
            isBallGoingRight = true;
        }
        public async Task MoveBallObject()
        {
            await Task.Run(async () =>
            {
                try
                {
                    while (GetRuntime() == true)
                    {
                        //set the position of the cursor
                        //print the ball at the position of the cursor
                        Print(ballTile, ballX, ballY);

                        //the sleep function sets the time between 2 print operations
                        //if the sleep function is not used the ball will basicly play the game
                        //at a speed too high for humans to interact with.
                        Thread.Sleep(100);

                        //print empty space
                        Print(" ", ballX, ballY);

                        //if the ball is going down, increase the Y position
                        //if the ball is going right, increase the X position
                        if (isBallGoingDown)
                        {
                            ballY++;
                        }
                        else if (!isBallGoingDown)
                        {
                            ballY--;
                        }
                        if (isBallGoingRight)
                        {
                            ballX++;
                        }
                        else if (!isBallGoingRight)
                        {
                            ballX--;
                        }

                        //if the ball is at the top or bottom of the field
                        if (ballY == 1 || ballY == GetFieldWidth() - 1)
                        {
                            isBallGoingDown = !isBallGoingDown;
                        }

                        //if the ball is o the left of the field
                        if (ballX == 1)
                        {
                            //if the ball is next to the left racket, reverse the direction
                            if (ballY == GetLeftRacketY1() || ballY == GetLeftRacketY2() || ballY == GetLeftRacketY3())
                            {
                                isBallGoingRight = !isBallGoingRight;
                            }
                            else if (ballY != GetLeftRacketY1() || ballY != GetLeftRacketY2() || ballY != GetLeftRacketY3())
                            {
                                //if the ball is on the left of the field but above or below the left racket
                                SetRightPlayerScore(GetRightPlayerScore() + 1);
                                ballY = GetFieldWidth() / 2;
                                ballX = GetFieldLength() / 2;
                                await Print($"{GetLeftPlayerScore()} | {GetRightPlayerScore()}", GetScoreboardX(), GetScoreboardY());

                                //if the opposite player has more than 10 points he has won 
                                //gameover
                                if (GetRightPlayerScore() > 10)
                                {
                                    DisplayWinner();
                                    Thread.Sleep(99999);
                                }
                            }
                        }

                        //if the ball is on the right of the field
                        if (ballX == GetFieldLength() - 2)
                        {
                            //if the ball is next to the right racket, reverse the direction
                            if (ballY == GetRightRacketY1() || ballY == GetRightRacketY2() || ballY == GetRightRacketY3())
                            {
                                isBallGoingRight = !isBallGoingRight;
                            }
                            else if (ballY != GetRightRacketY1() || ballY != GetRightRacketY2() || ballY != GetRightRacketY3())
                            {
                                //if the ball is on the right of the field but above or below the right racket
                                //give the oposite player a point
                                SetLeftPlayerScore(GetLeftPlayerScore() + 1);
                                ballY = GetFieldWidth() / 2;
                                ballX = GetFieldLength() / 2;
                                await Print($"{GetLeftPlayerScore()} | {GetRightPlayerScore()}", GetScoreboardX(), GetScoreboardY());

                                //if the opposite player has more than 10 points he has won 
                                //gameover
                                if (GetLeftPlayerScore() > 10)
                                {
                                    DisplayWinner();
                                    Thread.Sleep(99999);
                                }
                            }
                        }
                        //Pause the thread before the logic is executed again
                        //if this sleep method is removed the game will play itself out in less than 1 sec
                        Thread.Sleep(500);
                    }
                }
                catch (AggregateException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            });
        }
    }
}