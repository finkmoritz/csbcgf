using Newtonsoft.Json;

namespace csbcgf
{
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

        protected Hand(List<ICard> cards)
        {
            this.cards = cards;
        }

        [JsonIgnore]
        public int MaxSize => 10;

        [JsonIgnore]
        public override List<ICard> AllCards => new List<ICard>(cards);

        [JsonIgnore]
        public override bool IsEmpty => cards.Count == 0;

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

        public ICard this[int index]
        {
            get => cards[index];
        }
    }
}
