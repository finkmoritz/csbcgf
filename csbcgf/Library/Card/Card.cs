using Newtonsoft.Json;

namespace csbcgf
{
    public abstract class Card : ReactiveCompound, ICard
    {
        [JsonProperty]
        protected IPlayer? owner;

        protected Card() {}

        public Card(bool initialize = true) : base(initialize)
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

        [JsonIgnore]
        public int ManaValue {
            get => Math.Max(0, Components.Sum(c => c.ManaValue));
            set
            {
                AddComponent(new CardComponent(value - Components.Sum(c => c.ManaValue), 0));
            }
        }

        [JsonIgnore]
        public int ManaBaseValue {
            get => Math.Max(0, Components.Sum(c => c.ManaBaseValue));
            set
            {
                AddComponent(new CardComponent(0, value - Components.Sum(c => c.ManaBaseValue)));
            }
        }

        public virtual bool IsCastable(IGameState gameState)
        {
            return owner != null
                && owner == gameState.ActivePlayer
                && owner.Hand.Contains(this)
                && ManaValue <= gameState.ActivePlayer.ManaValue;
        }
    }
}
