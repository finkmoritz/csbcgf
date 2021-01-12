using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class AddCardToBoardAction : Action
    {
        [JsonProperty]
        public readonly IBoard Board;

        [JsonProperty]
        public ICard Card;

        [JsonProperty]
        public int BoardIndex;

        [JsonConstructor]
        public AddCardToBoardAction(IBoard board, ICard card, int boardIndex)
        {
            Board = board;
            Card = card;
            BoardIndex = boardIndex;
        }

        public override void Execute(IGame game)
        {
            Board.AddAt(BoardIndex, Card);
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return Card != null
                && Board.IsFreeSlot(BoardIndex);
        }
    }
}
