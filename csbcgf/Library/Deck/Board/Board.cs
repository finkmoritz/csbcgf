using System;
using System.Collections.Generic;

namespace csbcgf
{
    [Serializable]
    public class Board : IBoard
    {
        public int MaxSize { get; private set; }

        /// <summary>
        /// Data container.
        /// </summary>
        protected ICard[] Cards;

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

        /// <summary>
        /// Represents all Cards on a Player's Board.
        /// </summary>
        public Board(int maxSize)
        {
            MaxSize = maxSize;

            Cards = new ICard[MaxSize];
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

        public bool IsEmpty
        {
            get
            {
                foreach (ICard card in Cards)
                {
                    if (card != null)
                    {
                        return false;
                    }
                }
                return true;
            }
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
