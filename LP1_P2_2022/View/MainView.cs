using LP1_P2_2022.Model;

namespace LP1_P2_2022.View
{
    public class MainView : IView
    {
        /// <summary>
        ///     Prints the menu options
        /// </summary>
        public void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("[1] Play");
            Console.WriteLine("[2] Rules");
            Console.WriteLine("[3] Quit");
            Console.WriteLine();
        }


        /// <summary>
        ///     Prints the game rules and waits for input to return to menu
        /// </summary>
        public void PrintRules()
        {
            Console.Clear();

            Console.WriteLine("  Game Rules");
            Console.WriteLine("  ----------\n");

            Console.WriteLine("  Both players start off the board, " +
                              "and alternate turns playing.");

            Console.WriteLine("  The first player to reach the " +
                              "last board space, wins the game.");

            Console.WriteLine("  The player advances on the board " +
                              "by rolling a D6 die.");

            Console.WriteLine("  If the player lands on a special location, " +
                              "it activates the following action:\n");

            Console.WriteLine("\tSnakes: Player goes one space below.");
            Console.WriteLine("\tLaders: Player goes one space above.");

            Console.WriteLine("\tCobra: Player go to the first " +
                              "space of the table.");

            Console.WriteLine("\tBoost: Player go foward two spaces.");
            Console.WriteLine("\tU-turn: Player go back two spaces.");

            Console.ReadKey();
        }


        /// <summary>
        ///     Prints the game table
        /// </summary>
        /// <param name="table">Game table</param>
        /// <param name="players">Players</param>
        /// <param name="actions">Actions that occurred</param>
        public void PrintTable(Table table, Player playerTurn,
         Player[] players, string actions)
        {
            Console.Clear();

            // Iterates the game table, and prints each space
            for (int i = table.Y - 1; i >= 0; i--)
            {
                if (i % 2 == 0)
                    for (int j = 0; j < table.X; j++)
                    {
                        Console.BackgroundColor = GetColor(table.Spaces[i, j]);
                        Console.Write($" {i * table.X + j + 1:D2} ");
                    }
                else
                    for (int j = table.X - 1; j >= 0; j--)
                    {
                        Console.BackgroundColor = GetColor(table.Spaces[i, j]);
                        Console.Write($" {i * table.X + j + 1:D2} ");
                    }

                Console.ResetColor();
                Console.WriteLine();
            }

            // Draw the players
            DrawPlayers(table, players);
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(actions);

            // Prints the table colors description
            Description();
            Console.WriteLine($"Turn: {playerTurn.Appearance} | " +
            $"Extra Die: {(playerTurn.ExtraDie ? "Has" : "Doesn't have")} | " +
            $"Cheat Die: {(playerTurn.CheatDie ? "Has" : "Doesn't have")}");
        }


        /// <summary>
        ///     Draws the players on the table
        /// </summary>
        /// <param name="table">Game table</param>
        /// <param name="players">Players</param>
        private void DrawPlayers(Table table, Player[] players)
        {
            // Current cursor position
            int[] cursorPos = { Console.CursorLeft, Console.CursorTop };

            int xPos = players[0].Position[0];
            int yPos = players[0].Position[1];

            // If the player is on the table
            if (xPos >= 0)
            {
                Console.BackgroundColor = GetColor(table.Spaces[yPos, xPos]);

                xPos = yPos % 2 == 0 ? xPos * 4 : 4 * table.X - xPos * 4 - 4;
                yPos = 4 - yPos;

                // Places the cursor where the player is going to be drawn
                Console.SetCursorPosition(xPos, yPos);

                Console.Write($" {players[0].Appearance}  ");
            }

            xPos = players[1].Position[0];
            yPos = players[1].Position[1];

            // If the player is on the table
            if (xPos >= 0)
            {
                Console.BackgroundColor = GetColor(table.Spaces[yPos, xPos]);

                xPos = yPos % 2 == 0 ? xPos * 4 : 4 * table.X - xPos * 4 - 4;
                yPos = 4 - yPos;

                // Places the cursor where the player is going to be drawn
                Console.SetCursorPosition(xPos, yPos);

                Console.Write($" {players[1].Appearance}  ");
            }

            Console.ResetColor();

            // Places the cursor where it was before drawing the players
            Console.SetCursorPosition(cursorPos[0], cursorPos[1]);
        }


        /// <summary>
        ///     Prints the table colors description
        /// </summary>
        private void Description()
        {
            // Foreach enum value (Space type)
            foreach (int spaceValue in Enum.GetValues(typeof(Space)))
            {
                Space space = (Space)spaceValue;

                // Set space color
                Console.BackgroundColor = GetColor(space);
                Console.Write("  ");
                Console.ResetColor();
                Console.Write($" {space}\t");
            }

            Console.WriteLine("\n");
            Console.ResetColor();
        }


        /// <summary>
        ///     Gets the color of a given space
        /// </summary>
        /// <param name="space">Space to get the color from</param>
        /// <returns></returns>
        private ConsoleColor GetColor(Space space)
        {
            return space switch
            {
                Space.Snakes => ConsoleColor.Red,
                Space.Ladders => ConsoleColor.Blue,
                Space.Cobra => ConsoleColor.Green,
                Space.Boost => ConsoleColor.Cyan,
                Space.UTurn => ConsoleColor.DarkMagenta,
                Space.ExtraDie => ConsoleColor.Yellow,
                Space.CheatDie => ConsoleColor.DarkYellow,
                _ => ConsoleColor.Black
            };
        }


        /// <summary>
        ///     Prints an error message
        /// </summary>
        /// <param name="errorName">Error type</param>
        public void PrintError(string errorName)
        {
            // Error output
            string error;

            error = errorName switch
            {
                "menu" =>
                    "Not a valid menu option, use the numeric options. [1] [2] [3]",
                "input" => "Invalid input.",
                "extra" => "You don't own an Extra Die",
                _ => "Error!"
            };

            Console.WriteLine(error + "\n");

            Console.ReadKey();
        }


        /// <summary>
        ///     Prints the game end
        /// </summary>
        /// <param name="winner">Game winner</param>
        public void PrintGameEnd(Player winner)
        {
            Console.WriteLine($"Player {winner.Appearance} won the game!");
            Console.ReadKey();
        }


        /// <summary>
        ///     Reads the user input
        /// </summary>
        /// <returns>User input in lowercase and without white spaces</returns>
        public string ReadInput()
        {
            return Console.ReadLine().Trim().ToLower();
        }
    }
}