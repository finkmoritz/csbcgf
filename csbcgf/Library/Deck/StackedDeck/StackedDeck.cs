using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class StackedDeck : IStackedDeck
    {
        [JsonProperty]
        protected Stack<ICard> cards = new Stack<ICard>();

        [JsonIgnore]
        public List<ICard> AllCards => new List<ICard>(cards);

        public StackedDeck()
        {
        }

        [JsonIgnore]
        public int Size => cards.Count;

        [JsonIgnore]
        public bool IsEmpty
        {
            get => cards.Count == 0;
        }

        public bool Contains(ICard card)
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
