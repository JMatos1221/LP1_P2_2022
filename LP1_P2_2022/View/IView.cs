using LP1_P2_2022.Model;

namespace LP1_P2_2022.View;

public interface IView
{
    void PrintMenu();
    void PrintRules();
    void PrintTable(Table table, Player playerTurn,
    Player[] players, string actions);
    void PrintError(string errorName);
    void PrintGameEnd(Player winner);
    string ReadInput();
}