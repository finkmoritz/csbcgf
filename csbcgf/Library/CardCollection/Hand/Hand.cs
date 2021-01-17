using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class Hand : CardCollection, IHand
    {
        /// <summary>
        /// Data container.
        /// </summary>
        [JsonProperty]
        protected List<ICard> cards;

        public Hand() : this(new List<ICard>())
        {
        }

        [JsonConstructor]
        protected Hand(List<ICard> cards)
        {
            this.cards = cards;
        }

        [JsonIgnore]
        public int MaxSize { get => 10; }

        [JsonIgnore]
        public override List<ICard> AllCards => new List<ICard>(cards);

        [JsonIgnore]
        public override bool IsEmpty
        {
            get => cards.Count == 0;
        }

        [JsonIgnore]
        public override int Size => cards.Count;

        public override bool Contains(ICard card)
        {
            return cards.Contains(card);
        }

        public void Add(ICard card)
        {
            if(cards.Count < MaxSize)
            {
                cards.Add(card);
            }
        }

        public void Remove(ICard card)
        {
            cards.Remove(card);
        }

        public override object Clone()
        {
            List<ICard> cardsClone = new List<ICard>();
            foreach (ICard card in cards)
            {
                cardsClone.Add((ICard)card.Clone());
            }
            return new Hand(cardsClone);
        }

        public ICard this[int index]
        {
            get => cards[index];
        }
    }
}
