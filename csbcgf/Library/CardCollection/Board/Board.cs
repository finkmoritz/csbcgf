using System;
using System.Collections.Generic;

namespace csbcgf
{
    public class Board : CardCollection, IBoard
    {
        /// <summary>
        /// Data container.
        /// </summary>
        protected ICard[] cards;

        protected const int MaximumCapacity = 6;

        /// <summary>
        /// Represents all Cards on a Player's Board.
        /// </summary>
        public Board() : this(new ICard[MaximumCapacity])
        {
            for (int i = 0; i < cards.Length; ++i)
            {
                cards[i] = null;
            }
        }

        protected Board(ICard[] cards)
        {
            this.cards = cards;
        }

        public int MaxSize { get => MaximumCapacity; }

        public override List<ICard> AllCards
        {
            get
            {
                List<ICard> allCards = new List<ICard>();
                foreach (ICard card in cards)
                {
                    if (card != null)
                    {
                        allCards.Add(card);
                    }
                }
                return allCards;
            }
        }

        public override bool IsEmpty
        {
            get
            {
                foreach (ICard card in cards)
                {
                    if (card != null)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public override int Size
        {
            get
            {
                int size = 0;
                foreach (ICard card in cards)
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
            get => cards[index];
        }

        public override bool Contains(ICard card)
        {
            foreach (ICard c in cards)
            {
                if (c == card)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddAt(int index, ICard card)
        {
            if(!IsFreeSlot(index))
            {
                throw new CsbcgfException("Cannot add card to board, because " +
                    "position " + index + " is already occupied!");
            }
            cards[index] = card;
        }

        public void Remove(ICard card)
        {
            for(int i=0; i<cards.Length; ++i)
            {
                if(cards[i] == card)
                {
                    cards[i] = null;
                }
            }
        }

        public bool IsFreeSlot(int index)
        {
            return cards[index] == null;
        }
    }
}
