using Newtonsoft.Json;

namespace csbcgf
{
    public class CastMonsterAction : Action
    {
        [JsonProperty]
        protected IPlayer player = null!;

        [JsonProperty]
        protected IMonsterCard monsterCard = null!;

        [JsonProperty]
        protected int boardIndex;

        protected CastMonsterAction() {}

        public CastMonsterAction(IPlayer player, IMonsterCard monsterCard,
            int boardIndex, bool isAborted = false
            ) : base(isAborted)
        {
            this.player = player;
            this.monsterCard = monsterCard;
            this.boardIndex = boardIndex;
        }

        [JsonIgnore]
        public IPlayer Player {
            get => player;
        }

        [JsonIgnore]
        public IMonsterCard MonsterCard {
            get => monsterCard;
        }

        [JsonIgnore]
        public int BoardIndex {
            get => boardIndex;
        }

        public override void Execute(IGame game)
        {
            game.Execute(new ModifyManaStatAction(Player, -MonsterCard.ManaValue, 0));
            game.Execute(new RemoveCardFromHandAction(Player.Hand, MonsterCard));
            game.Execute(new AddCardToBoardAction(Player.Board, MonsterCard, BoardIndex));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return MonsterCard.IsSummonable(gameState)
                && Player.Board.IsFreeSlot(BoardIndex);
        }
    }
}
