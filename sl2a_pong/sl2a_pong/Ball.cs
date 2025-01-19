namespace sl2a_pong
{
    public class Ball : PongHandler
    {
        public Ball()
        {
            //call the ball movement algorithm
            CallBallMovementAlgorithm();
        }
        private async Task CallBallMovementAlgorithm()
        {
            //declare & initialise local method variables
            int x = GetFieldLength() / 2;
            int y = GetFieldWidth() /2;
            bool isBallGoingDown = true;
            bool isBallGoingRight = true;

            await Task.Run(async () =>
            {
                try
                {
                    while (GetRuntime() == true)
                    {
                        //run the ball movement algorithm
                        //give the return values to the SetPlayerScore method
                        var result = await MoveBallObject("O", x, y, isBallGoingDown, isBallGoingRight);

                        //set the current player positions
                        x = result.x;
                        y = result.y;
                        isBallGoingDown = result.down;
                        isBallGoingRight = result.right;

                        await SetPlayerScore(result.playerScore, result.player);
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
        private async Task<(bool playerScore, int player, int x, int y, bool down, bool right)> MoveBallObject(String ballTilePar, int ballxPar, int ballyPar, bool ballGoingDownPar, bool ballGoingRight)
        {
            //declare & initialise local variables
            int x = ballxPar;
            int y = ballyPar;
            bool isBallGoingDown = ballGoingDownPar;
            bool isBallGoingRight = ballGoingRight;

            //set the position of the cursor
            //print the ball at the position of the cursor
            await Print(ballTilePar, x, y);

            //the Delay function sets the time between 2 print operations
            //if the Delay function is not used the ball will bassicaly play the game
            //at a speed too high for humans to interact with.
            await Task.Delay(100);

            //print empty space
            //await Print(" ", x, y);

            //if the ball is at the top or bottom of the field
            if (y < 2)
            {
                isBallGoingDown = !isBallGoingDown;
            }else if (y > GetFieldWidth() - 2) 
            {
                isBallGoingDown = !isBallGoingDown;
            }

            //if the ball IS NOT at the left AND NOT at the right of the field
            if (x > 1 && x < (GetFieldLength() -1)) 
            {
                if (y > 0 && y < (GetFieldWidth()))
                {
                    //if the ball is going down, increase the Y position
                    //if the ball is going up, decrease the Y position
                    //if the ball is going right, increase the X position
                    //if the ball is going left, decrease the X position
                    if (isBallGoingDown)
                    {
                        y++;
                    }
                    else if (!isBallGoingDown)
                    {
                        y--;
                    }
                    if (isBallGoingRight)
                    {
                        x++;
                    }
                    else if (!isBallGoingRight)
                    {
                        x--;
                    }
                }
            }

            //if the ball is on the left of the field
            if (x < 2)
            {
                //if the ball is next to the left racket, reverse the direction
                if (y == GetLeftRacketY1() || y == GetLeftRacketY2() || y == GetLeftRacketY3())
                {
                    isBallGoingRight = !isBallGoingRight;
                }
                else if (y != GetLeftRacketY1() && y != GetLeftRacketY2() && y != GetLeftRacketY3())
                {
                    //if the ball is on the left of the field but above or below the left racket
                    //the right player gets a point and reset the ball position
                    int temporaryPositionX = x;
                    int temporaryPositionY = y;

                    y = GetFieldWidth() / 2;
                    x = GetFieldLength() / 2;

                    return (true, 0, x, y, isBallGoingDown, isBallGoingRight);
                }
            }

            //if the ball is on the right of the field
            if (x >= GetFieldLength() - 1)
            {
                //if the ball is next to the right racket, reverse the direction
                if (y == GetRightRacketY1() || y == GetRightRacketY2() || y == GetRightRacketY3())
                {
                    isBallGoingRight = !isBallGoingRight;
                }
                else if (y != GetRightRacketY1() && y != GetRightRacketY2() && y != GetRightRacketY3())
                {
                    //if the ball is on the right of the field but above or below the right racket
                    //give the oposite player a point and reset the ball position
                    int temporaryPositionX = x;
                    int temporaryPositionY = y;

                    y = GetFieldWidth() / 2;
                    x = GetFieldLength() / 2;

                    return (true, 1, x, y, isBallGoingDown, isBallGoingRight);
                }
            }

            //Pause the thread before the logic is executed again
            //if this Delay method is removed, the game will play itself out in less than 1 sec
            await Task.Delay(500);

            //if no player has scored, return false
            return (false, 0,x,y,isBallGoingDown,isBallGoingRight);
        }
    }
}