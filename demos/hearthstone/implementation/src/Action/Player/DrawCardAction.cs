using csbcgf;
using Newtonsoft.Json;

namespace hearthstone
{
    public class DrawCardAction : csbcgf.Action<HearthstoneGameState>
    {
        [JsonProperty]
        protected IPlayer player = null!;

        [JsonProperty]
        protected ICard? drawnCard;

        protected DrawCardAction() { }

        public DrawCardAction(IPlayer player, bool isAborted = false)
            : base(isAborted)
        {
            this.player = player;
        }

        [JsonIgnore]
        public IPlayer Player
        {
            get => player;
        }

        [JsonIgnore]
        public ICard? DrawnCard
        {
            get => drawnCard;
        }

        public override void Execute(IGame<HearthstoneGameState> game)
        {
            drawnCard = player.GetCardCollection(CardCollectionKeys.Deck).Last;
            game.ExecuteSequentially(new List<IAction> {
                new RemoveCardFromCardCollectionAction(player.GetCardCollection(CardCollectionKeys.Deck), drawnCard),
                new AddCardToCardCollectionAction(player.GetCardCollection(CardCollectionKeys.Hand), drawnCard)
            });
        }

        public override bool IsExecutable(HearthstoneGameState gameState)
        {
            return !player.GetCardCollection(CardCollectionKeys.Deck).IsEmpty;
        }
    }
}
