using System;
using System.Collections.Generic;

namespace csccgl
{
    [Serializable]
    public class Hand : IHand
    {
        public const int MaxCards = 10;
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

        public ICard RemoveAt(int index)
        {
            ICard card = Cards[index];
            Cards.RemoveAt(index);
            return card;
        }
    }
}
