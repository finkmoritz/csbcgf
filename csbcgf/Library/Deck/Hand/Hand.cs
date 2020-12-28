using System;
using System.Collections.Generic;

namespace csbcgf
{
    [Serializable]
    public class Hand : IHand
    {
        public int MaxSize => 10;

        /// <summary>
        /// Data container.
        /// </summary>
        protected List<ICard> Cards = new List<ICard>();

        public List<ICard> AllCards => new List<ICard>(Cards);

        public Hand()
        {
        }

        public void Add(ICard card)
        {
            if(Cards.Count < MaxSize)
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

        public int Size => Cards.Count;

        public ICard this[int index]
        {
            get => Cards[index];
            set => Cards[index] = value;
        }
    }
}
