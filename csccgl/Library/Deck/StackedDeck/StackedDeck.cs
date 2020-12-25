using System;
using System.Collections.Generic;
using System.Linq;

namespace csccgl
{
    [Serializable]
    public class StackedDeck : IStackedDeck
    {
        protected Stack<ICard> Cards = new Stack<ICard>();

        public StackedDeck()
        {
        }

        public bool IsEmpty()
        {
            return Cards.Count == 0;
        }

        public ICard PopCard()
        {
            return Cards.Pop();
        }

        public void PushCard(ICard card)
        {
            Cards.Push(card);
        }

        public void Shuffle()
        {
            ICard[] tmp = Cards.ToArray();
            Cards.Clear();
            foreach (ICard card in tmp.OrderBy(x => new Random().Next()))
            {
                Cards.Push(card);
            }
        }
    }
}
