namespace csbcgf
{
    public class DieAction : Action
    {
        public IMonsterCard MonsterCard;

        public DieAction(IMonsterCard monsterCard, bool isAborted = false)
            : base(isAborted)
        {
            MonsterCard = monsterCard;
        }

        public override void Execute(IGame game)
        {
            IPlayer owner = MonsterCard.FindParentPlayer(game)!;
            game.Execute(new RemoveCardFromBoardAction(owner.Board, MonsterCard));
            game.Execute(new AddCardToGraveyardAction(owner.Graveyard, MonsterCard));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            IPlayer? owner = MonsterCard.FindParentPlayer(gameState);
            return owner != null
                && owner.Board.Contains(MonsterCard);
        }
    }
}
