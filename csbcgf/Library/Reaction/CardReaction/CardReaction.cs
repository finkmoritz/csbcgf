using Newtonsoft.Json;

namespace csbcgf
{
    public abstract class CardReaction<T, TGame, TAction> : Reaction<T, TGame, TAction>, ICardReaction<T, TGame, TAction>
        where T : IGameState
        where TGame : IGame<T>
        where TAction : IAction<T>
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
