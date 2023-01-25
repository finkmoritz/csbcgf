using Newtonsoft.Json;

namespace csbcgf
{
    public class AddCardToGraveyardAction : Action
    {
        [JsonProperty]
        protected IDeck graveyard = null!;

        [JsonProperty]
        public ICard card = null!;

        protected AddCardToGraveyardAction() {}

        public AddCardToGraveyardAction(IDeck graveyard, ICard card, bool isAborted = false)
            : base(isAborted)
        {
            this.graveyard = graveyard;
            this.card = card;
        }

        [JsonIgnore]
        public IDeck Graveyard {
            get => graveyard;
        }

        [JsonIgnore]
        public ICard Card {
            get => card;
        }

        public override void Execute(IGame game)
        {
            Graveyard.Push(Card);
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return Card != null && !Graveyard.Contains(Card);
        }
    }
}
