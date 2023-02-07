using csbcgf;
using Newtonsoft.Json;

namespace hearthstone
{
    public class DieAction : csbcgf.Action
    {
        [JsonProperty]
        protected IMonsterCard monsterCard = null!;

        protected DieAction() { }

        public DieAction(IMonsterCard monsterCard, bool isAborted = false)
            : base(isAborted)
        {
            this.monsterCard = monsterCard;
        }

        [JsonIgnore]
        public IMonsterCard MonsterCard
        {
            get => monsterCard;
        }

        public override void Execute(IGame game)
        {
            IPlayer owner = MonsterCard.Owner!;
            game.ActionQueue.ExecuteSequentially(new List<IAction> {
                new RemoveCardFromCardCollectionAction(owner.GetCardCollection(CardCollectionKeys.Board), MonsterCard),
                new AddCardToCardCollectionAction(owner.GetCardCollection(CardCollectionKeys.Graveyard), MonsterCard)
            });
        }

        public override bool IsExecutable(IGameState gameState)
        {
            IPlayer? owner = MonsterCard.Owner;
            return owner != null
                && owner.GetCardCollection(CardCollectionKeys.Board).Contains(MonsterCard);
        }
    }
}
