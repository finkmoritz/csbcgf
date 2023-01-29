using Newtonsoft.Json;

namespace csbcgf
{
    public abstract class CardCollection : ICardCollection
    {
        [JsonProperty]
        protected IPlayer? owner;
        
        /// <summary>
        /// Abstract representation of a collection of Cards.
        /// </summary>
        public CardCollection()
        {
        }

        [JsonIgnore]
        public IPlayer? Owner {
            get {
                return owner;
            }
            set {
                owner = value;
            }
        }

        public abstract int Size { get; }
        public abstract List<ICard> AllCards { get; }
        public abstract bool IsEmpty { get; }
        public abstract bool Contains(ICard card);
    }
}
