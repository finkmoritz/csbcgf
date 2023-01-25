using Newtonsoft.Json;

namespace csbcgf
{
    public class RemoveCardFromDeckAction : Action
    {
        [JsonProperty]
        protected ICard? card;

        [JsonProperty]
        protected IDeck deck = null!;

        protected RemoveCardFromDeckAction() {}

        public RemoveCardFromDeckAction(IDeck deck, ICard? card = null, bool isAborted = false)
            : base(isAborted)
        {
            this.deck = deck;
            this.card = card;
        }

        [JsonIgnore]
        public ICard? Card {
            get => card;
        }

        [JsonIgnore]
        public IDeck Deck {
            get => deck;
        }

        public override void Execute(IGame game)
        {
            card = Deck.Pop();
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return !deck.IsEmpty;
        }
    }
}
