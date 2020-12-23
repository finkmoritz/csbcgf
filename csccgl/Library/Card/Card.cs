using System;

namespace csccgl
{
    [Serializable]
    public class Card : ICard
    {
        public ManaStat ManaStat { get; }

        public Card(int mana)
        {
            ManaStat.Value = mana;
        }
    }
}
