using LP1_P2_2022.Model;
using LP1_P2_2022.View;

namespace LP1_P2_2022.Controller
{
    public class MainController
    {
        // Game table
        private Table _table;

        // Random number generated
        private Random _rnd;

        // Players array
        private Player[] _players;

        // Current player turn
        private Player _playerTurn;

        // Stores the random die value
        private int dieValue;

        // List of chained actions
        private string actions;


        /// <summary>
        ///     Controller constructor, receives table instance
        /// </summary>
        /// <param name="table">Game table</param>
        public MainController(Table table)
        {
            _table = table;
        }


        /// <summary>
        ///     Sets up the game table and players
        /// </summary>
        private void Setup()
        {
            // Create players
            _players = new[] { new Player('A'), new Player('B') };

            // Sets player turn to the first player
            _playerTurn = _players[0];

            // Fill the table with spaces of type Normal
            for (int i = 0; i < _table.Spaces.GetLength(0); i++)
                for (int j = 0; j < _table.Spaces.GetLength(1); j++)
                    _table.SetSpace(i, j, Space.Normal);

            _rnd = new Random();

            // Generate Snakes
            GenerateSpace(Space.Snakes, 2, 4, 1, _table.X);

            // Generate Ladders
            GenerateSpace(Space.Ladders, 2, 4, 0, _table.X - 1);

            // Generate Cobras
            GenerateSpace(Space.Cobra, 1, 1, 2, _table.X);

            // Generate Boosts
            GenerateSpace(Space.Boost, 0, 2, 0, _table.X - 1);

            // Generate U-Turns
            GenerateSpace(Space.UTurn, 0, 2, 1, _table.X);
            // Generate ExtraDie
            GenerateSpace(Space.ExtraDie, 1, 1, 0, _table.X);
            // Generate CheatDie
            GenerateSpace(Space.CheatDie, 1, 1, 0, _table.X);
        }


        /// <summary>
        ///     Generate a random amount of a given space type within the
        ///     given amount and row limits
        /// </summary>
        /// <param name="space">Space to generate</param>
        /// <param name="min">Minimum amount that can be generated</param>
        /// <param name="max">Maximum amount that can be generated</param>
        /// <param name="rowMin">Lowest row to place space</param>
        /// <param name="rowMax">Highest row to place space</param>
        private void GenerateSpace(Space space, int min, int max, int rowMin,
                                   int rowMax)
        {
            /// <summary>
            /// Amount of generated spaces of the specified type
            /// </summary>
            /// <returns>Random number in the given range</returns>
            int amount = _rnd.Next(min, max + 1);

            // Get random table positions until the given amount
            // of the space type is generated
            for (int i = 0; i < amount;)
            {
                int x = _rnd.Next(0, _table.Y);
                int y = _rnd.Next(rowMin, rowMax);

                // Continue the loop if it's not a Normal space or if
                // it is the first or last space of the board
                if (_table.Spaces[y, x] != Space.Normal ||
                    (x == 0 && y == 0) ||
                    (x == _table.X - 1 && y == _table.Y - 1))
                    continue;

                // Sets the table space
                _table.SetSpace(x, y, space);
                i++;
            }
        }


        /// <summary>
        ///     /// Gameloop, runs endlessly until the player quits the game
        /// </summary>
        /// <param name="view">
        ///     View object that outputs data and receives input
        ///     from the user
        /// </param>
        public void CoreLoop(IView view)
        {
            bool menu = true;
            bool game = false;

            // Menu loop
            do
            {
                view.PrintMenu();

                string option = view.ReadInput();

                // Switch case to read the selected option
                switch (option)
                {
                    case "1":
                        Setup();

                        view.PrintTable(_table, _players, "");

                        game = true;

                        break;

                    case "2":
                        view.PrintRules();

                        break;

                    case "3":
                        menu = false;

                        break;

                    default:
                        view.PrintError("menu");

                        break;
                }

                // Game loop
                while (game)
                {
                    view.ReadInput();

                    MovementDie(_playerTurn);
                    game = CheckGameEnd();

                    view.PrintTable(_table, _players,
                                    actions);

                    if (!game) view.PrintGameEnd(_playerTurn);
                }
            } while (menu);
        }


        /// <summary>
        ///     Player movement with random die value
        /// </summary>
        /// <param name="player">Player to move (current player's turn)</param>
        private void MovementDie(Player player)
        {
            actions = string.Empty;

            dieValue = _rnd.Next(1, 7);

            player.Position[0] += dieValue;

            actions +=
                $"Player {_playerTurn.Appearance}: Die = {dieValue}; " +
                $"Advanced {dieValue} positions to a ";

            ClampPlayer(player);

            SpaceAction(player);
        }


        /// <summary>
        ///     Clamps the player's position, making sure he's placed on the board
        /// </summary>
        /// <param name="player">Player whose position needs clamping</param>
        private void ClampPlayer(Player player)
        {
            // While player is not on the board
            while (player.Position[0] > _table.X - 1 ||
                   player.Position[0] < 0)

                // If player exceeds the last space (winning space)
                if (player.Position[1] == _table.Y - 1 &&
                    player.Position[0] > 0)
                {
                    player.Position[0] =
                        _table.X - 1 -
                        (player.Position[0] - (_table.X - 1));
                }

                // If player needs to go to the lower row
                else if (player.Position[0] < 0)
                {
                    // If player is on the lowest row (off the table)
                    if (player.Position[1] == 0)
                        return;

                    player.Position[1] -= 1;
                    player.Position[0] += _table.X;
                }
                /*Else, player is moved normally to 
                the next row (the row above)*/
                else
                {
                    player.Position[1] += 1;
                    player.Position[0] -= _table.X;
                }
        }


        /// <summary>
        ///     Trigger special space action
        /// </summary>
        /// <param name="target">Player to apply the action to</param>
        private void SpaceAction(Player target)
        {
            // If player stops at the same space as the other player
            if (_players[0].Position[0] == _players[1].Position[0] &&
                _players[0].Position[1] == _players[1].Position[1])
            {
                actions +=
                    $"{_table.Spaces[target.Position[1], target.Position[0]]} ";

                // Changes target to opponent
                target = _playerTurn.Appearance == _players[0].Appearance
                             ? _players[1]
                             : _players[0];

                // Opponent moves backwards 1 position
                target.Position[0] -= 1;

                actions +=
                    $"location; Player {target.Appearance} was there and " +
                    "was moved back 1 position to a ";
            }

            // Clamp target, to make sure he's on board
            ClampPlayer(target);

            // If targe is on  board
            if (target.Position[0] >= 0)
            {
                Space currentSpace =
                    _table.Spaces[target.Position[1], target.Position[0]];

                // Trigger action on target
                if (currentSpace == Space.Normal)
                {
                    actions += "Normal location.\n";

                    return;
                }

                // Select special action to apply to
                // the player based on space type
                switch (currentSpace)
                {
                    case Space.Snakes:
                        SnakesAction(target);

                        actions +=
                            "Snakes special location; Moved down 1 row to a ";

                        break;

                    case Space.Ladders:
                        LaddersAction(target);

                        actions +=
                            "Ladders special location; Moved up 1 row to a ";

                        break;

                    case Space.Cobra:
                        CobraAction(target);

                        actions += "Cobra special location; " +
                                   "Moved back to the start to a ";

                        break;

                    case Space.Boost:
                        BoostAction(target);

                        actions +=
                            "Boost special location; " +
                            "Moved forward 2 positions to a ";

                        break;

                    case Space.UTurn:
                        UTurnAction(target);

                        actions +=
                            "U-Turn special location; " +
                            "Moved back 2 positions to a ";

                        break;
                }

                // Recursive call to trigger new space action, not called if
                // this is a Normal space (returns instead)
                SpaceAction(target);
            }
        }


        /// <summary>
        ///     Snakes action, moves the player to the lower row
        /// </summary>
        /// <param name="target">Player to apply the action to</param>
        private void SnakesAction(Player target)
        {
            target.Position[1] -= 1;
            target.Position[0] = _table.X - 1 - target.Position[0];
        }


        /// <summary>
        ///     Ladder action, moves the player to the upper row
        /// </summary>
        /// <param name="target">Player to apply the action to</param>
        private void LaddersAction(Player target)
        {
            target.Position[1] += 1;
            target.Position[0] = _table.X - 1 - target.Position[0];
        }


        /// <summary>
        ///     Cobra action, moves the player to the first table position (0, 0)
        /// </summary>
        /// <param name="target">Player to apply the action to</param>
        private void CobraAction(Player target)
        {
            target.Position = new[] { 0, 0 };
        }


        /// <summary>
        ///     Boost action, moves the player 2 spaces forward
        /// </summary>
        /// <param name="target">Player to apply the action to</param>
        private void BoostAction(Player target)
        {
            target.Position[0] += 2;
        }


        /// <summary>
        ///     U-Turn action, moves the player 2 spaces backwards
        /// </summary>
        /// <param name="target">Player to apply the action to</param>
        private void UTurnAction(Player target)
        {
            target.Position[0] -= 2;
        }


        /// <summary>
        ///     Checks if the game as ended, if not, changes player turn
        /// </summary>
        /// <returns> true if game is still running, false if not</returns>
        private bool CheckGameEnd()
        {
            if (_playerTurn.Position[0] != _table.X - 1 ||
                _playerTurn.Position[1] != _table.Y - 1)
            {
                ChangeTurn();

                return true;
            }

            return false;
        }


        /// <summary>
        ///     Changes player turn based on _playerTurn variable
        /// </summary>
        private void ChangeTurn()
        {
            _playerTurn = _playerTurn.Appearance == _players[0].Appearance
                              ? _players[1]
                              : _players[0];
        }
    }
}