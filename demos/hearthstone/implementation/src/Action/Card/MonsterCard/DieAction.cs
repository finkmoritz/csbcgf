using csbcgf;
using Newtonsoft.Json;

namespace hearthstone
{
    public class DieAction : csbcgf.Action<HearthstoneGameState>
    {
        [JsonProperty]
        protected HearthstoneMonsterCard monsterCard = null!;

        protected DieAction() { }

        public DieAction(HearthstoneMonsterCard monsterCard, bool isAborted = false)
            : base(isAborted)
        {
            this.monsterCard = monsterCard;
        }

        [JsonIgnore]
        public HearthstoneMonsterCard MonsterCard
        {
            get => monsterCard;
        }

        public override void Execute(IGame<HearthstoneGameState> game)
        {
            IPlayer owner = MonsterCard.Owner!;
            game.ExecuteSequentially(new List<IAction> {
                new RemoveCardFromCardCollectionAction(owner.GetCardCollection(CardCollectionKeys.Board), MonsterCard),
                new AddCardToCardCollectionAction(owner.GetCardCollection(CardCollectionKeys.Graveyard), MonsterCard)
            });
        }

        public override bool IsExecutable(HearthstoneGameState gameState)
        {
            IPlayer? owner = MonsterCard.Owner;
            return owner != null
                && owner.GetCardCollection(CardCollectionKeys.Board).Contains(MonsterCard);
        }
    }
}
