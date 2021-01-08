using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class RemoveCardFromBoardAction : IAction
    {
        [JsonProperty]
        protected readonly IBoard board;

        [JsonProperty]
        protected readonly ICard card;

        [JsonConstructor]
        public RemoveCardFromBoardAction(IBoard board, ICard card)
        {
            this.board = board;
            this.card = card;
        }

        public void Execute(IGame game)
        {
            board.Remove(card);
        }

        public bool IsExecutable(IGame gameState)
        {
            return board.Contains(card);
        }
    }
}
