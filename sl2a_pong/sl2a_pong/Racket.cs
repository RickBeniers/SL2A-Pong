namespace sl2a_pong
{
    public class Racket : PongHandler
    {
        //declare variables
        private string racketTile;
        private int racketY1;
        private int racketY2;
        private int racketY3;
        private int fieldLength;
        private int fieldWidth;
        private int indicator;
        private bool runtime;

        //Class constructor
        public Racket(int ParIndicator)
        {
            //initialise variables
            //Get the required variables from the PongHandler class
            fieldLength = GetFieldLength();
            fieldWidth = GetFieldWidth();
            racketTile = "|";
            racketY1 = 4;
            racketY2 = 5;
            racketY3 = 6;

            //set the runtime variable
            runtime = GetRuntime();

            //The indicator is the indication which side of the game this racket should be, Left(1) or Right(0)
            indicator = ParIndicator;

            //Start the DetectMovement method
            DetectMovement(runtime);

            //Start the PrintRacket method
            PrintRacket();

        }

        //function to print the racket on the screen
        public async Task PrintRacket()
        {
            await Task.Run(async () =>
            {
                try
                {
                    while (GetRuntime() == true)
                    {
                        //check if the game is still running
                        runtime = GetRuntime();

                        if (indicator == 1)
                        {
                            //loop through the fieldWidth
                            //Print the racket at the correct positions
                            for (int i = 1; i < fieldWidth; i++)
                            {
                                try
                                {
                                    if (i == racketY1)
                                    {
                                        await Print(racketTile, 0, i);
                                    }
                                    if (i == racketY2)
                                    {
                                        await Print(racketTile, 0, i);
                                    }
                                    if (i == racketY3)
                                    {
                                        await Print(racketTile, 0, i);
                                    }
                                    if (i != racketY1 && i != racketY2 && i != racketY3)
                                    {
                                        await Print(" ", 0, i);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine(e.StackTrace);
                                }
                            }
                        }
                        else if (indicator == 0)
                        {
                            //loop through the fieldWidth
                            //Print the racket at the correct positions
                            for (int i = 1; i < fieldWidth; i++)
                            {
                                try
                                {
                                    if (i == racketY1)
                                    {
                                        await Print(racketTile, fieldLength - 1, i);
                                    }
                                    if (i == racketY2)
                                    {
                                        await Print(racketTile, fieldLength - 1, i);
                                    }
                                    if (i == racketY3)
                                    {
                                        await Print(racketTile, fieldLength - 1, i);
                                    }
                                    if (i != racketY1 && i != racketY2 && i != racketY3)
                                    {
                                        await Print(" ", fieldLength - 1, i);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine(e.StackTrace);
                                }
                            }
                        }
                        SetRacketPositions(this.racketY1, this.racketY2, this.racketY3, this.indicator);
                    }
                    //small delay to prevent high CPU usage
                    await Task.Delay(50);
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

        //method to detect the movement of the racket
        public async Task DetectMovement(bool runtimePar) 
        {
            await Task.Run(() => 
            {
                try
                {
                    while (runtime == true)
                    {
                        var key = Console.ReadKey(true).Key;
                        if (indicator == 1)
                        {
                            switch (key)
                            {
                                //if one of the players presses the up, down, W or S keys than
                                //change the corresponding height of the racket
                                case ConsoleKey.W:
                                    //if the racket is at the bottom of the field
                                    if ((racketY1 == fieldWidth - 3) && (racketY2 == fieldWidth - 2) && (racketY3 == fieldWidth - 1))
                                    {
                                        //move the racket up
                                        this.racketY1--;
                                        this.racketY2--;
                                        this.racketY3--;
                                    }
                                    //if the racket is between the top and bottom of the field
                                    if ((racketY1 > 1) && (racketY2 > 2) && (racketY3 > 3))
                                    {
                                        //move the racket up
                                        this.racketY1--;
                                        this.racketY2--;
                                        this.racketY3--;
                                    }
                                    break;
                                case ConsoleKey.S:
                                    //if the racket is at the top of the field
                                    if (racketY1 == 1 && racketY2 == 2 && racketY3 == 3)
                                    {
                                        //move the racket down
                                        this.racketY1++;
                                        this.racketY2++;
                                        this.racketY3++;
                                    }
                                    //if the racket is between the top and bottom of the field
                                    if ((racketY1 < fieldWidth - 3) && (racketY2 < fieldWidth - 2) && (racketY3 < fieldWidth - 1) && (racketY3 < fieldWidth - 1))
                                    {
                                        //move the racket down
                                        this.racketY1++;
                                        this.racketY2++;
                                        this.racketY3++;
                                    }
                                    break;
                            }
                        }
                        else if (indicator == 0)
                        {
                            switch (key)
                            {
                                //if one of the players presses the up, down, W or S keys than
                                //change the corresponding height of the racket
                                case ConsoleKey.UpArrow:
                                    //if the racket is at the bottom of the field
                                    if ((racketY1 == fieldWidth - 3) && (racketY2 == fieldWidth - 2) && (racketY3 == fieldWidth - 1))
                                    {
                                        //move the racket up
                                        this.racketY1--;
                                        this.racketY2--;
                                        this.racketY3--;
                                    }
                                    //if the racket is between the top and bottom of the field
                                    if ((racketY1 > 1) && (racketY2 > 2) && (racketY3 > 3))
                                    {
                                        //move the racket up
                                        this.racketY1--;
                                        this.racketY2--;
                                        this.racketY3--;
                                    }
                                    break;
                                case ConsoleKey.DownArrow:
                                    //if the racket is at the top of the field
                                    if (racketY1 == 1 && racketY2 == 2 && racketY3 == 3)
                                    {
                                        //move the racket down
                                        this.racketY1++;
                                        this.racketY2++;
                                        this.racketY3++;
                                    }
                                    //if the racket is between the top and bottom of the field
                                    if ((racketY1 < fieldWidth - 3) && (racketY2 < fieldWidth - 2) && (racketY3 < fieldWidth - 1) && (racketY3 < fieldWidth - 1))
                                    {
                                        //move the racket down
                                        this.racketY1++;
                                        this.racketY2++;
                                        this.racketY3++;
                                    }
                                    break;
                            }
                        }
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
