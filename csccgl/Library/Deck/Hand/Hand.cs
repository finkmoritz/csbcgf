using System;
using System.Collections.Generic;

namespace csccgl
{
    [Serializable]
    public class Hand : IHand
    {
        /// <summary>
        /// Maximum number of Cards that this Hand can hold.
        /// </summary>
        public const int MaxCards = 10;

        /// <summary>
        /// Data container.
        /// </summary>
        protected List<ICard> Cards = new List<ICard>();

        public Hand()
        {
        }

        public void Add(ICard card)
        {
            if(Cards.Count < MaxCards)
            {
                Cards.Add(card);
            }
        }

        public bool Contains(ICard card)
        {
            return Cards.Contains(card);
        }

        public bool IsEmpty()
        {
            return Cards.Count == 0;
        }

        public void Remove(ICard card)
        {
            Cards.Remove(card);
        }
    }
}
