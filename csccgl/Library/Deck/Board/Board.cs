using System;
using System.Collections.Generic;

namespace csccgl
{
    [Serializable]
    public class Board : IBoard
    {
        public int MaxSize => MaxCapacity;

        /// <summary>
        /// Data container.
        /// </summary>
        protected ICard[] Cards = new ICard[MaxCapacity];

        public List<ICard> AllCards => new List<ICard>(Cards);

        private const int MaxCapacity = 6;

        /// <summary>
        /// Represents all Cards on a Player's Board.
        /// </summary>
        public Board()
        {
        }

        public void AddAt(int index, ICard card)
        {
            if(!IsFreeSlot(index))
            {
                throw new CsccglException("Cannot add card to board, because " +
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

        public ICard Get(int index) => Cards[index];

        public bool IsFreeSlot(int index) => Cards[index] == null;

        public int Size {
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
    }
}
