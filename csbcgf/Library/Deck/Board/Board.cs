using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class Board : IBoard
    {
        public int MaxSize { get; protected set; }

        /// <summary>
        /// Data container.
        /// </summary>
        [JsonProperty]
        protected ICard[] cards;

        [JsonIgnore]
        public List<ICard> AllCards
        {
            get
            {
                List<ICard> allCards = new List<ICard>();
                foreach (ICard card in cards)
                {
                    if(card != null)
                    {
                        allCards.Add(card);
                    }
                }
                return allCards;
            }
        }

        [JsonIgnore]
        public bool IsEmpty
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

        [JsonIgnore]
        public int Size
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

        /// <summary>
        /// Represents all Cards on a Player's Board.
        /// </summary>
        public Board(int maxSize)
        {
            MaxSize = maxSize;

            cards = new ICard[MaxSize];
            for (int i = 0; i < cards.Length; ++i)
            {
                cards[i] = null;
            }
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

        public bool Contains(ICard card)
        {
            foreach(ICard c in cards)
            {
                if(c == card)
                {
                    return true;
                }
            }
            return false;
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

        public ICard this[int index]
        {
            get => cards[index];
            set => cards[index] = value;
        }
    }
}
