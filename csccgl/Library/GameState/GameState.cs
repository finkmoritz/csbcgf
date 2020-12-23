using System;

namespace csccgl
{
    [Serializable]
    public class GameState : IGameState
    {
        public readonly Player[] players;

        public GameState(Player[] players)
        {
            this.players = players;
        }
    }
}
