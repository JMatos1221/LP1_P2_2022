namespace LP1_P2_2022.Model
{
    public class Player
    {
        /// <summary>
        ///     Player table representation
        /// </summary>
        private char _appearance;

        /// <summary>
        ///     Player position
        /// </summary>
        private int[] _position;

        /// <summary>
        /// Special Dies
        /// </summary>
        private bool extraDie;
        private bool cheatDie;

        /// <summary>
        ///     Player appearance getter property
        /// </summary>
        public char Appearance => _appearance;

        /// <summary>
        ///     Player position getter/setter property
        /// </summary>
        /// <value>_position</value>
        public int[] Position
        {
            get => _position;
            set => _position = value;
        }

        public bool ExtraDie
        {
            get => extraDie;
            set { extraDie = value; }
        }
        public bool CheatDie
        {
            get => cheatDie;
            set { cheatDie = value; }
        }


        /// <summary>
        ///     Player constructor
        /// </summary>
        /// <param name="appearance">Player appearance char</param>
        public Player(char appearance)
        {
            _appearance = appearance;
            _position = new[] { -1, 0 };
            extraDie = false;
            cheatDie = false;
        }
    }
}