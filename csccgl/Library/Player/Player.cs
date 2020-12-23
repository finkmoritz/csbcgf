using System;

namespace csccgl
{
    [Serializable]
    public class Player : IPlayer
    {
        public readonly Deck deck;
        public readonly Deck hand;
        public readonly Deck board;
        public readonly Deck graveyard;

        public Player()
        {
        }
    }
}
