namespace LP1_P2_2022.Model;

public class SaveManager
{
    // File name
    private const string SaveName = "game.save";

    /// <summary>
    /// Saves the current game state
    /// </summary>
    /// <param name="table">Game table</param>
    /// <param name="players">Players</param>
    /// <param name="playerTurn">Current player turn</param>
    public void Save(Table table, Player[] players, Player playerTurn)
    {
        // Create/Overwrite save file
        using (StreamWriter sw =
               new(Environment.CurrentDirectory + @"\" + SaveName))
        {
            // Save spaces
            for (int i = 0; i < table.Spaces.GetLength(0); i++)
            {
                for (int j = 0; j < table.Spaces.GetLength(1); j++)
                    sw.Write((int)table.Spaces[i, j] + ",");

                sw.WriteLine();
            }

            // Save players' dies
            sw.WriteLine($"{players[0].ExtraDie},{players[0].CheatDie},");
            sw.WriteLine($"{players[1].ExtraDie},{players[1].CheatDie},");

            // Save current player's turn
            sw.WriteLine(playerTurn.Appearance);
        }

        Console.Write("\nGame Saved!");
        Console.ReadLine();
    }

    /// <summary>
    /// Loads the game from file
    /// </summary>
    /// <param name="table">Game Table</param>
    /// <param name="players">Players</param>
    /// <param name="playerTurn">Current player turn</param>
    public bool Load(Table table, Player[] players, out Player playerTurn)
    {
        if (!File.Exists(Environment.CurrentDirectory + @"\" + SaveName))
        {
            playerTurn = players[0];
            return false;
        }

        string line;

        // Start reading the file
        using (StreamReader sr = new(Environment.CurrentDirectory + @"\" + SaveName))
        {
            // Read and load table positions
            for (int i = 0; i < table.Spaces.GetLength(0); i++)
            {
                line = sr.ReadLine();

                string[] spaces = line.Split(",", StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < table.Spaces.GetLength(1); j++)
                {
                    int spaceInt = int.Parse(spaces[j]);

                    table.Spaces[i, j] = (Space)spaceInt;
                }
            }

            // Read player special dies
            line = sr.ReadLine();

            string[] dies = line.Split(",", StringSplitOptions.RemoveEmptyEntries);

            players[0].ExtraDie = bool.Parse(dies[0]);
            players[0].CheatDie = bool.Parse(dies[1]);

            line = sr.ReadLine();

            dies = line.Split(",", StringSplitOptions.RemoveEmptyEntries);

            players[1].ExtraDie = bool.Parse(dies[0]);
            players[1].CheatDie = bool.Parse(dies[1]);

            // Load current player turn
            line = sr.ReadLine().Trim();

            if (line == "A")
                playerTurn = players[0];
            else
                playerTurn = players[1];

        }
        return true;
    }
}