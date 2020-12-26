using System;
namespace csccgl
{
    public class GameOptions
    {
        /// <summary>
        /// Number of Cards that each Player draws at the start of a Game.
        /// </summary>
        public int InitialHandSize = 4;

        /// <summary>
        /// Players start a Game with the given Life value.
        /// </summary>
        public int InitialPlayerLife = 30;

        /// <summary>
        /// Container that holds information necessary to change a
        /// Game's behaviour.
        /// </summary>
        public GameOptions()
        {
        }
    }
}
