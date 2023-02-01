using System.Collections.Immutable;
using Newtonsoft.Json;

namespace csbcgf
{
    public class CardCollection : ICardCollection
    {
        [JsonProperty]
        protected IPlayer? owner;

        [JsonProperty]
        protected int? maxSize;

        [JsonProperty]
        protected List<ICard> cards = null!;

        protected CardCollection() { }

        public CardCollection(int? maxSize = null)
        {
            this.maxSize = maxSize;
            cards = new List<ICard>();
        }

        [JsonIgnore]
        public IEnumerable<ICard> Cards => cards.ToImmutableList();

        [JsonIgnore]
        public bool IsEmpty => cards.Count == 0;

        [JsonIgnore]
        public bool IsFull => maxSize != null && cards.Count >= maxSize;

        [JsonIgnore]
        public int Size => cards.Count;

        [JsonIgnore]
        public IPlayer? Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }

        [JsonIgnore]
        public int? MaxSize
        {
            get
            {
                return maxSize;
            }
            set
            {
                maxSize = value;
            }
        }

        [JsonIgnore]
        public ICard this[int index]
        {
            get => cards[index];
        }

        [JsonIgnore]
        public ICard First
        {
            get => cards[0];
        }

        [JsonIgnore]
        public ICard Last
        {
            get => cards[cards.Count - 1];
        }

        public bool Contains(ICard card)
        {
            return cards.Contains(card);
        }

        public void Add(ICard card)
        {
            if (MaxSize != null && cards.Count >= MaxSize)
            {
                throw new CsbcgfException("Cannot add ICard to CardCollection is its maximum size has been reached");
            }
            cards.Add(card);
            card.Owner = Owner;
        }

        public void Remove(ICard card)
        {
            cards.Remove(card);
            card.Owner = null;
        }

        public void Shuffle()
        {
            ICard[] cardsCopy = cards.ToArray();
            cards.Clear();
            foreach (ICard card in cardsCopy.OrderBy(x => new Random().Next()))
            {
                Add(card);
            }
        }
    }
}
