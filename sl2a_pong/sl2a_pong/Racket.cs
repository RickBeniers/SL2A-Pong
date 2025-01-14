using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace sl2a_pong
{
   public class Racket : PongHandler 
   {
      //declare variables
      private string racketTile;
      private int racketLength;
      private int leftRacketHeight;
      private int rightRacketHeight;
      private int fieldLength, fieldWidth;
      private int indicator;
      
      //Class constructor
      public Racket(int ParIndicator) 
      {
        //initialise variables
        //Get the required variables from the PongHandler class
        fieldLength = GetFieldLength();
        fieldWidth = GetFieldWidth();

        racketLength = fieldWidth / 4;
        racketTile = "|";
        leftRacketHeight = 0;
        rightRacketHeight = 0;

        //The indicator is the indication wich side of the game this racket shoud be, Left(1) or Right(0)
        indicator = ParIndicator;

        //Create thread to execute the print method.
        Thread racketPrinting = new Thread(PrintRacket);
        racketPrinting.Start();
      }
      
      //function to print the racket on the screen
      public void PrintRacket() 
      {
          while (GetRuntime() == true)
          {
              for (int i = 0; i < racketLength; i++)
              {
                 //print the racket that the player will use to deflect the ball
                 Console.SetCursorPosition(0, i + 1 + leftRacketHeight);
                 //Console.WriteLine(racketTile);
                 Print(racketTile);
                 Console.SetCursorPosition(fieldLength - 1, i + 1 + rightRacketHeight);
                 //Console.WriteLine(racketTile);
                 Print(racketTile);
              }
          }
      }

      //function to move the racket up and down
      public void MoveRacket() 
      {
          switch (Console.ReadKey().Key)
          {
             //if one of the players presses the up, down, W or S keys than
             //change the corresponding height of the racket
             case ConsoleKey.UpArrow:
                if (indicator == 1 && rightRacketHeight > 0)
                {
                    rightRacketHeight--;
                }
             break;
             case ConsoleKey.DownArrow:
                if (indicator == 1 && rightRacketHeight < fieldWidth - racketLength - 1)
                {
                    rightRacketHeight++;
                }
             break;
             case ConsoleKey.W:
                if (indicator == 0 && leftRacketHeight > 0)
                {
                    leftRacketHeight--;
                }
             break;
             case ConsoleKey.S:
                if (indicator == 0 && leftRacketHeight < fieldWidth - racketLength - 1)
                {
                    leftRacketHeight++;
                }
             break;
          }
      }
      //getters and setters to share required variables between classes
      public int GetLeftRacketHeight() 
      {
            return this.leftRacketHeight;
      }
      public int GetRightRacketHeight() 
      {
            return this.rightRacketHeight;
      }
   }
    
}
