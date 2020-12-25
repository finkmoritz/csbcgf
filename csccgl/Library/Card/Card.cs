using System;

namespace csccgl
{
    [Serializable]
    public abstract class Card : ICard
    {
        public ManaStat ManaStat { get; }

        /// <summary>
        /// Abstract class to represent a Card.
        /// </summary>
        /// <param name="mana">Initial value for the ManaStat.</param>
        public Card(int mana)
        {
            ManaStat.Value = mana;
        }

        public abstract bool IsPlayable(IGame game);
    }
}
