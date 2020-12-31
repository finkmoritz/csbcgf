using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class Hand : IHand
    {
        public int MaxSize { get; protected set; }

        /// <summary>
        /// Data container.
        /// </summary>
        [JsonProperty]
        protected List<ICard> cards = new List<ICard>();

        [JsonIgnore]
        public List<ICard> AllCards => new List<ICard>(cards);

        [JsonIgnore]
        public bool IsEmpty
        {
            get => cards.Count == 0;
        }

        [JsonIgnore]
        public int Size => cards.Count;


        public Hand(int maxSize)
        {
            MaxSize = maxSize;
        }

        public void Add(ICard card)
        {
            if(cards.Count < MaxSize)
            {
                cards.Add(card);
            }
        }

        public bool Contains(ICard card)
        {
            return cards.Contains(card);
        }

        public void Remove(ICard card)
        {
            cards.Remove(card);
        }

        public ICard this[int index]
        {
            get => cards[index];
            set => cards[index] = value;
        }
    }
}
