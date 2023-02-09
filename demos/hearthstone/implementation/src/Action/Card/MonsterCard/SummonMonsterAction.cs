using csbcgf;
using Newtonsoft.Json;

namespace hearthstone
{
    public class SummonMonsterAction : csbcgf.Action<HearthstoneGameState>
    {
        [JsonProperty]
        protected IPlayer player = null!;

        [JsonProperty]
        protected HearthstoneMonsterCard monsterCard = null!;

        protected SummonMonsterAction() { }

        public SummonMonsterAction(IPlayer player, HearthstoneMonsterCard monsterCard, bool isAborted = false
            ) : base(isAborted)
        {
            this.player = player;
            this.monsterCard = monsterCard;
        }

        [JsonIgnore]
        public IPlayer Player
        {
            get => player;
        }

        [JsonIgnore]
        public HearthstoneMonsterCard MonsterCard
        {
            get => monsterCard;
        }

        public override void Execute(IGame<HearthstoneGameState> game)
        {
            game.ExecuteSequentially(new List<IAction> {
                new ModifyManaStatAction(Player, -MonsterCard.ManaValue, 0),
                new RemoveCardFromCardCollectionAction(Player.GetCardCollection(CardCollectionKeys.Hand), MonsterCard),
                new AddCardToCardCollectionAction(Player.GetCardCollection(CardCollectionKeys.Board), MonsterCard)
            });
        }

        public override bool IsExecutable(HearthstoneGameState gameState)
        {
            return MonsterCard.IsSummonable(gameState)
                && !Player.GetCardCollection(CardCollectionKeys.Board).IsFull;
        }
    }
}
