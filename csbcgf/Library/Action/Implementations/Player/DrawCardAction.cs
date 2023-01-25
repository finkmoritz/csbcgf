using Newtonsoft.Json;

namespace csbcgf
{
    public class DrawCardAction : Action
    {
        [JsonProperty]
        protected IPlayer player = null!;

        [JsonProperty]
        protected ICard? drawnCard;

        protected DrawCardAction() {}

        public DrawCardAction(IPlayer player, bool isAborted = false)
            : base(isAborted)
        {
            this.player = player;
        }

        [JsonIgnore]
        public IPlayer Player {
            get => player;
        }

        [JsonIgnore]
        public ICard? DrawnCard {
            get => drawnCard;
        }

        public override void Execute(IGame game)
        {
            RemoveCardFromDeckAction removeAction = new RemoveCardFromDeckAction(player.Deck);
            game.Execute(removeAction);
            drawnCard = removeAction.Card;
            game.Execute(new AddCardToHandAction(player.Hand, drawnCard!));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return !player.Deck.IsEmpty;
        }
    }
}
