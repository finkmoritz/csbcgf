using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class RemoveCardFromBoardAction : IAction
    {
        [JsonProperty]
        protected IBoard board;

        [JsonProperty]
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

        public bool IsExecutable(IGame game)
        {
            return board.Contains(card);
        }
    }
}
