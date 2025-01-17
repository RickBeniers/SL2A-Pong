using System;

namespace sl2a_pong
{
    public class Menu : ProgramBase
    {
        // declare variables
        private String dateAndTime;

        public Menu()
        {
            DisplayMenuBox();
            dateAndTime = "";
        }
        public void DisplayMenuBox()
        {
            // Define the box size
            int boxWidth = 50;
            int boxHeight = 20;

            // Top border
            Print(new string('═', boxWidth));

            // Menu content
            string[] menuItems = {
                "Main Menu",
                "",
                "1. Play pong",
                "2. Display Date and Time",
                "3. Exit",
                "",
                "Select an option: "
            };

            foreach (string item in menuItems)
            {
                try
                {
                    string paddedItem = item.PadRight(boxWidth - 2);
                    Print("║" + paddedItem.Substring(0, Math.Min(boxWidth - 2, paddedItem.Length)) + "║");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }

            // Fill the rest of the box with empty lines
            for (int i = menuItems.Length; i < boxHeight - 1; i++)
            {
                try
                {
                    Print("║" + new string(' ', boxWidth - 2) + "║");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }

            // Bottom border
            Print(new string('═', boxWidth));
        }

        public async Task DisplayDateTime()
        {
            await Task.Run(() =>
            {
                try
                {
                    Console.Clear();
                    dateAndTime = $"Current Date and Time: {DateTime.Now}";
                    Print();
                }
                catch (AggregateException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            });
        }

        // a method derived from ProgramBase to implement a method to print to the console
        public override void Print()
        {
            Console.WriteLine(dateAndTime);
        }
        public override void Print(String message) 
        {
            Console.WriteLine(message);
        }
    }
}
