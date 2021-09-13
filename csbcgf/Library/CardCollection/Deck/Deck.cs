using System;
using System.Collections.Generic;
using System.Linq;

namespace csbcgf
{
    public class Deck : CardCollection, IDeck
    {
        protected Stack<ICard> cards;

        public Deck() : this(new Stack<ICard>())
        {
        }

        protected Deck(Stack<ICard> cards)
        {
            this.cards = cards;
        }

        public override List<ICard> AllCards => new List<ICard>(cards);

        public override int Size => cards.Count;

        public override bool IsEmpty
        {
            get => cards.Count == 0;
        }

        public override bool Contains(ICard card)
        {
            return cards.Contains(card);
        }
        

        public ICard Pop()
        {
            return cards.Pop();
        }

        public void Push(ICard card)
        {
            cards.Push(card);
        }

        public void Shuffle()
        {
            ICard[] tmp = cards.ToArray();
            cards.Clear();
            foreach (ICard card in tmp.OrderBy(x => new Random().Next()))
            {
                cards.Push(card);
            }
        }
    }
}
