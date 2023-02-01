using Newtonsoft.Json;

namespace csbcgf
{
    public class RemoveCardFromCardCollectionAction : Action
    {
        [JsonProperty]
        protected ICardCollection cardCollection = null!;

        [JsonProperty]
        protected ICard card = null!;

        protected RemoveCardFromCardCollectionAction() { }

        public RemoveCardFromCardCollectionAction(ICardCollection cardCollection, ICard card, bool isAborted = false)
            : base(isAborted)
        {
            this.cardCollection = cardCollection;
            this.card = card;
        }

        [JsonIgnore]
        public ICardCollection CardCollection
        {
            get => cardCollection;
        }

        [JsonIgnore]
        public ICard Card
        {
            get => card;
        }

        public override void Execute(IGame game)
        {
            CardCollection.Remove(Card);
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return CardCollection.Contains(Card);
        }
    }
}
