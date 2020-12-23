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

        public ICard PopCard()
        {
            return (ICard)Cards.Pop();
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
