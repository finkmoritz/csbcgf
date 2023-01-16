namespace csbcgf
{
    public class RemoveCardFromBoardAction : Action
    {
        public readonly IBoard Board;

        public ICard Card;

        public RemoveCardFromBoardAction(IBoard board, ICard card, bool isAborted = false)
            : base(isAborted)
        {
            Board = board;
            Card = card;
        }

        public override void Execute(IGame game)
        {
            Board.Remove(Card);
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return Board.Contains(Card);
        }
    }
}
