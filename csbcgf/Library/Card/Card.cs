using Newtonsoft.Json;

namespace csbcgf
{
    public abstract class Card : ReactiveCompound, ICard
    {
        [JsonProperty]
        protected IPlayer? owner;

        protected Card() { }

        public Card(bool _ = true) : base(_)
        {
        }

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

        public virtual bool IsCastable(IGameState gameState)
        {
            return true;
        }
    }
}
