using LP1_P2_2022.Controller;
using LP1_P2_2022.Model;
using LP1_P2_2022.View;

namespace LP1_P2_2022
{
    internal class Program
    {
        /// <summary>
        ///     Main method, called when we run the program
        /// </summary>
        /// <param name="args">Console arguments</param>
        private static void Main(string[] args)
        {
            Program p = new();

            p.Run();
        }


        /// <summary>
        ///     Method that runs the game using the MVC pattern
        /// </summary>
        private void Run()
        {
            Table _table = new(5, 5);

            MainController mainController = new(_table);

            MainView mainView = new();

            mainController.CoreLoop(mainView);
        }
    }
}