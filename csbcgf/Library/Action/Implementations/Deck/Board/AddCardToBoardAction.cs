using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class AddCardToBoardAction : IAction
    {
        [JsonProperty]
        protected readonly IBoard board;

        [JsonProperty]
        protected readonly ICard card;

        [JsonProperty]
        protected readonly int boardIndex;

        [JsonConstructor]
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
