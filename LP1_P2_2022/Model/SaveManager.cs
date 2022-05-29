namespace LP1_P2_2022.Model;

public class SaveManager
{
    private const string SaveName = "game.save";


    public void Save(Table table, Player[] players, Player playerTurn)
    {
        using (StreamWriter sw =
               new(Environment.CurrentDirectory + @"\" + SaveName))
        {
            for (int i = 0; i < table.Spaces.GetLength(0); i++)
            {
                for (int j = 0; j < table.Spaces.GetLength(1); j++)
                    sw.Write((int)table.Spaces[i, j] + ", ");

                sw.WriteLine();
            }

            sw.WriteLine($"{players[0].ExtraDie},{players[0].CheatDie},");
            sw.WriteLine($"{players[1].ExtraDie},{players[1].CheatDie},");
            sw.WriteLine(playerTurn.Appearance);

            Console.Write("\nGame Saved!");
            Console.ReadLine();
        }
    }
}