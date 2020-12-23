using System;

namespace csccgl
{
    [Serializable]
    public class Deck : IDeck
    {
        public readonly Card[] cards;

        public Deck()
        {
        }
    }
}
