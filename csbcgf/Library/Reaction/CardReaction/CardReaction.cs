using Newtonsoft.Json;

namespace csbcgf
{
    public abstract class CardReaction : Reaction, ICardReaction
    {
        [JsonProperty]
        protected ICard parentCard = null!;

        protected CardReaction() { }

        public CardReaction(ICard parentCard)
        {
            this.parentCard = parentCard;
        }

        [JsonIgnore]
        public ICard ParentCard
        {
            get
            {
                return parentCard;
            }
        }
    }
}
