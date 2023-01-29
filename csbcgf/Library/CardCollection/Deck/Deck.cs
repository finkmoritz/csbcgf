using Newtonsoft.Json;

namespace csbcgf
{
    public class Deck : CardCollection, IDeck
    {
        [JsonProperty]
        protected List<ICard> cards;

        public Deck() : this(new List<ICard>())
        {
        }

        protected Deck(List<ICard> cards)
        {
            this.cards = cards;
        }

        [JsonIgnore]
        public override List<ICard> AllCards => new List<ICard>(cards);

        [JsonIgnore]
        public override int Size => cards.Count;

        [JsonIgnore]
        public override bool IsEmpty => cards.Count == 0;

        public override bool Contains(ICard card)
        {
            return cards.Contains(card);
        }
        

        public ICard? Pop()
        {
            if (cards.Count == 0) {
                return null;
            }
            ICard card = cards.Last<ICard>();
            cards.Remove(card);
            return card;
        }

        public void Push(ICard card)
        {
            cards.Add(card);
            card.Owner = Owner;
        }

        public void Shuffle()
        {
            ICard[] tmp = cards.ToArray();
            cards.Clear();
            foreach (ICard card in tmp.OrderBy(x => new Random().Next()))
            {
                Push(card);
            }
        }
    }
}
