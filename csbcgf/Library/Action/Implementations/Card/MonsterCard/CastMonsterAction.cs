using Newtonsoft.Json;

namespace csbcgf
{
    public class CastMonsterAction : Action
    {
        [JsonProperty]
        protected IPlayer player = null!;

        [JsonProperty]
        protected IMonsterCard monsterCard = null!;

        protected CastMonsterAction() { }

        public CastMonsterAction(IPlayer player, IMonsterCard monsterCard, bool isAborted = false
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
        public IMonsterCard MonsterCard
        {
            get => monsterCard;
        }

        public override void Execute(IGame game)
        {
            game.Execute(new ModifyManaStatAction(Player, -MonsterCard.ManaValue, 0));
            game.Execute(new RemoveCardFromCardCollectionAction(Player.Hand, MonsterCard));
            game.Execute(new AddCardToCardCollectionAction(Player.Board, MonsterCard));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return MonsterCard.IsSummonable(gameState)
                && !Player.Board.IsFull;
        }
    }
}
