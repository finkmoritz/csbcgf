using System;
namespace csbcgf
{
    public class RemoveCardFromBoardAction : IAction
    {
        protected IBoard board;
        protected ICard card;

        public RemoveCardFromBoardAction(IBoard board, ICard card)
        {
            this.board = board;
            this.card = card;
        }

        public void Execute(IGame game)
        {
            board.Remove(card);
        }

        public bool IsExecutable(IGame game) => board.Contains(card);
    }
}
