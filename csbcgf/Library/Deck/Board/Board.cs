using System;
using System.Collections.Generic;

namespace csbcgf
{
    [Serializable]
    public class Board : IBoard
    {
        public int MaxSize => MaxCapacity;

        /// <summary>
        /// Data container.
        /// </summary>
        protected ICard[] Cards = new ICard[MaxCapacity];

        public List<ICard> AllCards
        {
            get
            {
                List<ICard> allCards = new List<ICard>();
                foreach (ICard card in Cards)
                {
                    if(card != null)
                    {
                        allCards.Add(card);
                    }
                }
                return allCards;
            }
        }

        private const int MaxCapacity = 6;

        /// <summary>
        /// Represents all Cards on a Player's Board.
        /// </summary>
        public Board()
        {
            for (int i = 0; i < Cards.Length; ++i)
            {
                Cards[i] = null;
            }
        }

        public void AddAt(int index, ICard card)
        {
            if(!IsFreeSlot(index))
            {
                throw new CsbcgfException("Cannot add card to board, because " +
                    "position " + index + " is already occupied!");
            }
            Cards[index] = card;
        }

        public bool Contains(ICard card)
        {
            foreach(ICard c in Cards)
            {
                if(c == card)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsEmpty()
        {
            foreach(ICard card in Cards)
            {
                if(card != null)
                {
                    return false;
                }
            }
            return true;
        }

        public void Remove(ICard card)
        {
            for(int i=0; i<Cards.Length; ++i)
            {
                if(Cards[i] == card)
                {
                    Cards[i] = null;
                }
            }
        }

        public bool IsFreeSlot(int index)
        {
            return Cards[index] == null;
        }

        public int Size
        {
            get {
                int size = 0;
                foreach (ICard card in Cards)
                {
                    if (card != null)
                    {
                        ++size;
                    }
                }
                return size;
            }
        }

        public ICard this[int index]
        {
            get => Cards[index];
            set => Cards[index] = value;
        }
    }
}
