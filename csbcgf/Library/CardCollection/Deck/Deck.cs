using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class Deck : CardCollection, IDeck
    {
        [JsonProperty]
        protected Stack<ICard> cards;

        public Deck() : this(new Stack<ICard>())
        {
        }

        [JsonConstructor]
        protected Deck(Stack<ICard> cards)
        {
            this.cards = cards;
        }

        [JsonIgnore]
        public override List<ICard> AllCards => new List<ICard>(cards);

        [JsonIgnore]
        public override int Size => cards.Count;

        [JsonIgnore]
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
