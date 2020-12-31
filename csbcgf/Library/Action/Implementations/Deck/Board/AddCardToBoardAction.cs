using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class AddCardToBoardAction : IAction
    {
        [JsonProperty]
        protected IBoard board;

        [JsonProperty]
        protected ICard card;

        [JsonProperty]
        protected int boardIndex;

        public AddCardToBoardAction(IBoard board, ICard card, int boardIndex)
        {
            this.board = board;
            this.card = card;
            this.boardIndex = boardIndex;
        }

        public void Execute(IGame game)
        {
            board.AddAt(boardIndex, card);
        }

        public bool IsExecutable(IGame game)
        {
            return card != null
                && board.IsFreeSlot(boardIndex);
        }
    }
}
