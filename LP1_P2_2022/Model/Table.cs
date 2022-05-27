namespace LP1_P2_2022.Model
{
    public class Table
    {
        /// <summary>
        ///     Array of spaces representing the table
        /// </summary>
        private Space[,] _spaces;

        /// <summary>
        ///     Table spaces getter property
        /// </summary>
        public Space[,] Spaces => _spaces;

        /// <summary>
        ///     Table width
        /// </summary>
        /// <value>Width</value>
        public int X { get; }

        /// <summary>
        ///     Table height
        /// </summary>
        /// <value>Height</value>
        public int Y { get; }


        /// <summary>
        ///     Table constructor
        /// </summary>
        /// <param name="x">Table width</param>
        /// <param name="y">Table height</param>
        public Table(int x, int y)
        {
            X = x;
            Y = y;

            _spaces = new Space[y, x];
        }


        /// <summary>
        ///     Sets table given coornidates with given space
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="space">Space type</param>
        public void SetSpace(int x, int y, Space space)
        {
            _spaces[y, x] = space;
        }
    }
}