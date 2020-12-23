using System;

namespace csccgl
{
    [Serializable]
    public class Game : IGame
    {
        /// <summary>
        /// Array of Players involved in the Game.
        /// </summary>
        public readonly Player[] Players;

        /// <summary>
        /// Index of the active Player. Refers to the Players array.
        /// Also see the ActivePlayer accessor.
        /// </summary>
        public readonly int ActivePlayerIndex;

        /// <summary>
        /// Additional GameOptions that help customizing a Game.
        /// </summary>
        public readonly GameOptions Options;


        /// <summary>
        /// Represent the current Game state and provides methods to alter
        /// this Game state.
        /// </summary>
        /// <param name="players"></param>
        /// <param name="options"></param>
        public Game(Player[] players, GameOptions options = null)
        {
            this.Players = players;
            this.ActivePlayerIndex = new Random().Next(this.Players.Length);
            this.Options = options;

            if(Options == null)
            {
                Options = new GameOptions();
            }

            Init();
        }

        /// <summary>
        /// Convenience method to retrieve the active Player.
        /// Equivalent to using
        /// <code>Players[ActivePlayerIndex]</code>
        /// </summary>
        public Player ActivePlayer => Players[ActivePlayerIndex];

        private void Init()
        {
            foreach (Player player in Players)
            {
                for (int i=0; i<Options.StartHandSize; ++i)
                {
                    player.DrawCard();
                }
            }
        }
    }
}
